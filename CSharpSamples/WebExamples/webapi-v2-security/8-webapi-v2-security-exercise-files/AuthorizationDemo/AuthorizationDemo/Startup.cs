using AuthorizationDemo.Security;
using Microsoft.Owin;
using Owin;
using Thinktecture.IdentityModel;

[assembly: OwinStartup(typeof(AuthorizationDemo.Startup))]

namespace AuthorizationDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ClaimsAuthorization.CustomAuthorizationManager = new AuthorizationManager();

            app.Use(typeof(AuthenticationSimulator));
            
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}