using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(WindowsAuthentication.Startup))]

namespace WindowsAuthentication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseWindowsAuthentication();

            app.UseClaimsTransformation(Transformation);
            app.UseWebApi(WebApiConfig.Register());
        }

        private async Task<ClaimsPrincipal> Transformation(ClaimsPrincipal incoming)
        {
            if (!incoming.Identity.IsAuthenticated)
            {
                return incoming;
            }

            var name = incoming.Identity.Name;

            // go to a datastore - find the app specific claims

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, name),
                new Claim(ClaimTypes.Role, "foo"),
                new Claim(ClaimTypes.Email, "foo@foo.com")
            };

            var id = new ClaimsIdentity("Windows");
            id.AddClaims(claims);

            return new ClaimsPrincipal(id);
        }
    }
}