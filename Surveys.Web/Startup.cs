using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Surveys.Web.App_Start;

[assembly: OwinStartup(typeof(Surveys.Web.Startup))]

namespace Surveys.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            OAuthConfig.ConfigureOAuth(app, config);
            app.UseWebApi(config);
            WebApiConfig.Register(config);
        }
    }
}
