using System.Web.Http;
using WebAPIWithGZipDeflate.Handlers;

namespace WebAPIWithGZipDeflate
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務
            config.MessageHandlers.Add(new DecompressorHandler());

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
