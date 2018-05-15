using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIWithGZipDeflate.Tests
{
    public class HttpClientHelper
    {
        public async Task<string> PostWithGZipByStream(string url, string data)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using (var client = new HttpClient(handler, false))
                {
                    var jsonBytes = Encoding.UTF8.GetBytes(data);
                    var ms = new Compressor().GZipReStream(jsonBytes);

                    StreamContent content = new StreamContent(ms);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    content.Headers.ContentEncoding.Add("gzip");

                    var response = await client.PostAsync(url, content).ConfigureAwait(false);
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return result;
                }
            }
        }

        public async Task<string> PostWithGZipByte(string url, string data)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using (var client = new HttpClient(handler, false))
                {
                    var jsonBytes = Encoding.UTF8.GetBytes(data);
                    var ms = new Compressor().GZipReByte(jsonBytes);

                    var byteArray = new ByteArrayContent(ms);
                    byteArray.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    byteArray.Headers.ContentEncoding.Add("gzip");

                    var response = await client.PostAsync(url, byteArray).ConfigureAwait(false);
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return result;
                }
            }
        }

        public async Task<string> PostWithDeflateStream(string url, string data)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using (var client = new HttpClient(handler, false))
                {
                    var jsonBytes = Encoding.UTF8.GetBytes(data);
                    var ms = new Compressor().DeflateReStream(jsonBytes);

                    StreamContent content = new StreamContent(ms);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    content.Headers.ContentEncoding.Add("deflate");

                    var response = await client.PostAsync(url, content).ConfigureAwait(false);
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return result;
                }
            }
        }

        public async Task<string> PostWithDeflateByte(string url, string data)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using (var client = new HttpClient(handler, false))
                {
                    var jsonBytes = Encoding.UTF8.GetBytes(data);
                    var ms = new Compressor().DeflateReByte(jsonBytes);

                    var byteArray = new ByteArrayContent(ms);
                    byteArray.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    byteArray.Headers.ContentEncoding.Add("deflate");

                    var response = await client.PostAsync(url, byteArray).ConfigureAwait(false);
                    var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return result;
                }
            }
        }
    }
}
