using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.IdentityModel;
using System.Text;
using System.Threading.Tasks;

namespace Identity.WindowsAuth
{
    class ClaimsTransformer : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (incomingPrincipal != null && incomingPrincipal.Identity.IsAuthenticated == true)
            {
                ((ClaimsIdentity)incomingPrincipal.Identity).AddClaim(new Claim(ClaimTypes.Role, "User"));
            }

            // SecurityException
            return CreatePrincipal(incomingPrincipal.Identity.Name);
        }

        private ClaimsPrincipal CreatePrincipal(string name)
        {
            var hasCastle = true;

            var claims = new List<Claim> {
                new Claim( ClaimTypes.Name,name)
                ,new Claim(@"http://dd",hasCastle.ToString())
                };
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));
            
            }

        }
    }

