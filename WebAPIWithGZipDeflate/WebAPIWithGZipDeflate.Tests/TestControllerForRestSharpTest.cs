using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAPIWithGZipDeflate.Tests
{
    [TestClass]
    public class TestControllerForRestSharpTest
    {
        private string filePath;
        private string data;

        [TestInitialize]
        public void Initialize()
        {
            filePath = Path.GetFullPath("testdata.json");
            data = File.ReadAllText(filePath);
        }

        [TestMethod]
        public void RestSharp_POST_40000_GZip_Data()
        {
            var expected = "\"OK\"";

            var client = new RestSharpHelper();
            var actual = client.PostWithGZip("http://localhost:8134/test", data);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RestSharp_POST_40000_Deflate_Data()
        {
            var expected = "\"OK\"";

            var client = new RestSharpHelper();
            var actual = client.PostWithDeflate("http://localhost:8134/test", data);

            Assert.AreEqual(expected, actual);
        }
    }
}
