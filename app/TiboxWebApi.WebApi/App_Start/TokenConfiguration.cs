using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using TiboxWebApi.WebApi.Provider;

namespace TiboxWebApi.WebApi
{
    public partial class Startup
    {
        //Esto trabaja a niveld e la aplicación
        public void ConfigureOAuth(IAppBuilder app)
        {
            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //conexiones seguras
                AllowInsecureHttp = true,//con certificado digital esto debe de ir false
                TokenEndpointPath = new PathString("/token"), //Ruta la cual vamos a solicitar el token
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1), //tiempo de expiración
                Provider = new SimpleAuthorizationServerProvider() //escogiendo el proveedor que se va usar para el token, en esta caso uno personalizado
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            //con todo esto le estamo diciendo a la webapi que la autenticaión va ser por token y no basica por user y pass

        }
    }
}