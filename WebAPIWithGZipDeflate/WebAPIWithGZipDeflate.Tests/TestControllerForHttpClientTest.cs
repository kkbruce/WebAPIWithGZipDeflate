using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAPIWithGZipDeflate.Tests
{
    [TestClass]
    public class TestControllerForHttpClientTest
    {
        private string filePath;
        private string data;
        private string url;

        [TestInitialize]
        public void Initialize()
        {
            filePath = Path.GetFullPath("testdata.json");
            data = File.ReadAllText(filePath);
            url = "http://localhost:8134/test";
        }

        [TestMethod]
        public void HttpClient_POST_40000_GZipByStream_Data()
        {
            var expected = "\"OK\"";

            var client = new HttpClientHelper();
            var actual = client.PostWithGZipByStream(url, data);

            Assert.AreEqual(expected, actual.Result);
        }

        [TestMethod]
        public void HttpClient_POST_40000_GZipByBytes_Data()
        {
            var expected = "\"OK\"";

            var client = new HttpClientHelper();
            var actual = client.PostWithGZipByte(url, data);

            Assert.AreEqual(expected, actual.Result);
        }

        [TestMethod]
        public void HttpClient_POST_40000_DeflateByStream_Data()
        {
            var expected = "\"OK\"";

            var client = new HttpClientHelper();
            var actual = client.PostWithDeflateStream(url, data);

            Assert.AreEqual(expected, actual.Result);
        }

        [TestMethod]
        public void HttpClient_POST_40000_DeflateByBytes_Data()
        {
            var expected = "\"OK\"";

            var client = new HttpClientHelper();
            var actual = client.PostWithDeflateByte(url, data);

            Assert.AreEqual(expected, actual.Result);
        }
    }
}
