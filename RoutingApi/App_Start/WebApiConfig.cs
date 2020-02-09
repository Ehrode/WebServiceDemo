using System.Net.Http.Headers;
using System.Web.Http;

namespace WebServiceDemo.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AppLaunch",
                routeTemplate: string.Empty,
                new
                {
                    controller = "Home",
                    action = "Welcome"
                }
            );

            config.Routes.MapHttpRoute(
                name: "CatchAll",
                routeTemplate: "{*url}",
                defaults: new { controller = "Error", action = "RouteNotFound"}
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
