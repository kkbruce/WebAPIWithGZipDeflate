using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIWithGZipDeflate.Models;

namespace WebAPIWithGZipDeflate.Controllers
{
    [Route("test")]
    public class TestController : ApiController
    {
        public HttpResponseMessage Post(DataModel data)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "OK");
        }
    }
}
