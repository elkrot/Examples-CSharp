using System.Web.Http;

namespace Thinktecture.Samples.Security
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // default API route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.EnableSystemDiagnosticsTracing();
        }
    }
}