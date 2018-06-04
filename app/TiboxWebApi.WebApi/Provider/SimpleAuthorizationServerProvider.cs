using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Security.Claims;
using TiboxWebApi.UnitOfWork;
using TiboxWebApi.WebApi.Utils;
using System;

namespace TiboxWebApi.WebApi.Provider
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUnitOfWork _unit;
        private ActiveDirectory _ad;

        public SimpleAuthorizationServerProvider()
        {
            _unit = new TiboxUnitOfWork();
            _ad = new ActiveDirectory();
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Aqui se hace la magia
            var form = await context.Request.ReadFormAsync();
            var user = new Models.User();

            if (string.Equals(form["tipo"], "lucas", StringComparison.OrdinalIgnoreCase))
            {
                user = null;
                user = _unit.Users.ValidateUser(context.UserName, context.Password);
            }
            else if(string.Equals(form["tipo"], "admin", StringComparison.OrdinalIgnoreCase))
            {
                bool validation = false;
                validation = _ad.Autenticado(context.UserName, context.Password);
                user = null;
                if (validation)
                {
                    user = _unit.Users.validateUserAD(context.UserName, context.Password);
                }
            }

            if (user == null)
            {
                context.SetError("invalid_grant", "Usuario o password incorrecto");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", context.UserName));

            context.Validated(identity);

        }
    }
}