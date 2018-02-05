using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(KatanaAuthentication.Startup))]

namespace KatanaAuthentication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseRequireTls(requireClientCertificate: true);
            app.UseBasicAuthentication("Demo", ValidateUsers);
            app.UseClientCertificateAuthentication(X509RevocationMode.NoCheck);

            app.UseWebApi(WebApiConfig.Register());
        }

        private Task<IEnumerable<Claim>> ValidateUsers(string id, string secret)
        {
            if (id == secret)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Role, "foo")
                };

                return Task.FromResult<IEnumerable<Claim>>(claims);
            }

            return Task.FromResult<IEnumerable<Claim>>(null);
        }
    }
}