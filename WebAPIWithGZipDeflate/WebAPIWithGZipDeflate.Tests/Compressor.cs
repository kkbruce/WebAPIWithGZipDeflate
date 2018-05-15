using System.IO;
using System.IO.Compression;

namespace WebAPIWithGZipDeflate.Tests
{
    public class Compressor
    {
        public byte[] GZipReByte(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    gzip.Write(data, 0, data.Length);
                }
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        public MemoryStream GZipReStream(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                gzip.Write(data, 0, data.Length);
            }
            ms.Position = 0;
            return ms;

        }

        public byte[] DeflateReByte(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (DeflateStream deflate = new DeflateStream(ms, CompressionMode.Compress, true))
                {
                    deflate.Write(data, 0, data.Length);
                }
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        public MemoryStream DeflateReStream(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            using (DeflateStream deflate = new DeflateStream(ms, CompressionMode.Compress, true))
            {
                deflate.Write(data, 0, data.Length);
            }
            ms.Position = 0;
            return ms;
        }
    }
}
