using System;
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
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content.Headers.ContentEncoding.Any())
            {
                var encodes = request.Content.Headers.ContentEncoding.ToList();
                foreach (var encode in encodes)
                {
                    if (encode.Equals("deflate", StringComparison.InvariantCultureIgnoreCase))
                    {
                        request.Content = await DecompressDeflateContentAsync(request).ConfigureAwait(false);
                        return await base.SendAsync(request, cancellationToken);
                    }
                    request.Content = await DecompressGzipContentAsync(request).ConfigureAwait(false);
                }
            }
            return await base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Decompresses the content of the Deflate.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private async Task<HttpContent> DecompressDeflateContentAsync(HttpRequestMessage request)
        {
            MemoryStream outputStream = new MemoryStream();
            var inputStream = await request.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using (var decompressor = new DeflateStream(inputStream, CompressionMode.Decompress))
            {
                await decompressor.CopyToAsync(outputStream).ConfigureAwait(false);
            }
            outputStream.Seek(0, SeekOrigin.Begin);

            HttpContent newContent = AddHeadersToNewContent(request, outputStream);
            return newContent;
        }

        /// <summary>
        /// Decompresses the content of the gzip.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private async Task<HttpContent> DecompressGzipContentAsync(HttpRequestMessage request)
        {
            MemoryStream outputStream = new MemoryStream();
            var inputStream = await request.Content.ReadAsStreamAsync().ConfigureAwait(false);
            using (var decompressor = new GZipStream(inputStream, CompressionMode.Decompress))
            {
                await decompressor.CopyToAsync(outputStream).ConfigureAwait(false);
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