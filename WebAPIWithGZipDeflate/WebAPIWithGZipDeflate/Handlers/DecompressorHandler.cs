﻿using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPIWithGZipDeflate.Handlers
{
    public class DecompressorHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content.Headers.ContentEncoding.Any())
            {
                var encodes = request.Content.Headers.ContentEncoding.ToList();
                foreach (var encode in encodes)
                {
                    if (encode.Equals("deflate", StringComparison.InvariantCultureIgnoreCase))
                    {
                        request.Content = DecompressDeflateContent(request);
                    }
                    request.Content = DecompressGzipContent(request);
                }
            }
            return base.SendAsync(request, cancellationToken);
        }

        private HttpContent DecompressDeflateContent(HttpRequestMessage request)
        {
            MemoryStream outputStream = new MemoryStream();
            var inputStream = request.Content.ReadAsStreamAsync().Result;
            using (var decompressor = new DeflateStream(inputStream, CompressionMode.Decompress))
            {
                decompressor.CopyToAsync(outputStream);
            }
            outputStream.Seek(0, SeekOrigin.Begin);

            HttpContent newContent = AddHeadersToNewContent(request, outputStream);
            return newContent;
        }

        private HttpContent DecompressGzipContent(HttpRequestMessage request)
        {
            MemoryStream outputStream = new MemoryStream();
            var inputStream = request.Content.ReadAsStreamAsync().Result;
            using (var decompressor = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                decompressor.CopyToAsync(outputStream);
            }
            outputStream.Seek(0, SeekOrigin.Begin);

            HttpContent newContent = AddHeadersToNewContent(request, outputStream);
            return newContent;
        }

        private HttpContent AddHeadersToNewContent(HttpRequestMessage request, MemoryStream outputStream)
        {
            HttpContent requestContent = request.Content;
            HttpContent newContent = new StreamContent(outputStream);

            foreach (var header in requestContent.Headers)
            {
                if (header.Key.ToLowerInvariant() == "content-encoding")
                {
                    newContent.Headers.Add(header.Key, "identity");
                    continue;
                }

                if (header.Key.ToLowerInvariant() == "content-length")
                {
                    newContent.Headers.Add(header.Key, outputStream.Length.ToString());
                    continue;
                }

                newContent.Headers.Add(header.Key, header.Value);
            }

            return newContent;
        }
    }
}