using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Routing;

namespace eSetter
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();
             var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors();  


            config.Routes.MapHttpRoute(
                name: "ActionWebApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new
                {
                    controller = "eSetter",
                    action = RouteParameter.Optional
                },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") }
            );

            config.Routes.MapHttpRoute(
                name: "ActionWebApiGet",
                routeTemplate: "api/{controller}/{action}",
                defaults: new
                {
                    controller = "eSetter",
                    action = RouteParameter.Optional
                },
                constraints: new { httpMethod = new HttpMethodConstraint("GET") }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    controller = "eSetter",
                    id = RouteParameter.Optional
                }
            );

            config.Formatters.Add(new BrowserJsonFormatter());
        }

        public class BrowserJsonFormatter : JsonMediaTypeFormatter
        {
            public BrowserJsonFormatter()
            {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
                this.SerializerSettings.Formatting = Formatting.Indented;
            }

            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
        }

    }
}
