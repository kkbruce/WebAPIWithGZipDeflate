using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIWithGZipDeflate.Tests
{
    public class RestSharpHelper
    {
        public string PostWithGZip(string url, string param)
        {
            byte[] jsonBytes = Encoding.UTF8.GetBytes(param);
            var gzipArray = new Compressor().GZipReByte(jsonBytes);

            var uri = url;
            var client = new RestClient(uri);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("content-encoding", "gzip");
            request.AddParameter("application/json", gzipArray, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(response.Content);
            }
            return response.Content;
        }

        public string PostWithDeflate(string url, string param)
        {
            byte[] jsonBytes = Encoding.UTF8.GetBytes(param);
            var deflateArray = new Compressor().DeflateReByte(jsonBytes);

            var uri = url;
            var client = new RestClient(uri);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("content-encoding", "deflate");
            request.AddParameter("application/json", deflateArray, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(response.Content);
            }
            return response.Content;
        }
    }
}
