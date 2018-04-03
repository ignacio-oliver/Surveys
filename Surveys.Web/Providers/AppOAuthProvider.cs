using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace Surveys.Web.Providers
{
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string publicClientId;

        public AppOAuthProvider(string publicClientId)
        {
            if(publicClientId == null)
            {
                throw new ArgumentNullException(nameof(publicClientId), "El identificador no es válido");
            }
            else
            {
                this.publicClientId = publicClientId;
            }
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-ControlAllow-Origin", new[] { "*" });

            //Aquí va el login REAL
            if ((context.UserName == "libro" && context.Password == "xamarin.forms") || 
                (context.UserName == "1" && context.Password == "2"))
            {
                //Crea y prepara el objeto ClaimsIdentity 
                var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, "Rodrigo Díaz Concha"));
                var data = new Dictionary<string, string> { { "email", "rodrigo@rdiazconcha.com" } };
                var properties = new AuthenticationProperties(data);

                //Crea un AuthenticationTicket con la identidad y las propiedades
                var ticket = new AuthenticationTicket(identity, properties);

                // Valida y autentica
                context.Validated(ticket);

                return Task.FromResult(true);
            }
            else
            {
                context.SetError("access_denied", "Acceso denegado");
                return Task.FromResult(false);
            }
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach(KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}