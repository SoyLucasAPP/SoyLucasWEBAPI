
using Owin;
using System.Web.Http;
namespace TiboxWebApi.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var configuration = new HttpConfiguration();

            Register(configuration);

            ConfigureOAuth(app);

            ConfigureInjector(configuration);            

            app.UseWebApi(configuration);
        }

        /* BASIC AUTHTENITCATION
         * -Toda webapi se pide medienta reclamos(claim)
          -Cada ves que se ejecute una tarea(TASK) el metodo debe de tener primero el valor de retorno
           async asi como tambien el metodo debe de ser llamado con un await delante.
        */

    }
}