using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Web.Http;
namespace TiboxWebApi.WebApi
{
    public partial class Startup
    {
        public void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            //Compreción
            config.MessageHandlers.Insert(0,
                new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor())
                );
            //Fin comprecion

            //Formato JSON
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            //Fin formato

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
