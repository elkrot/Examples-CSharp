using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.WindowsAuth
{
    public class ClaimsTransformation
    {
        internal void SetPrincipal()
        {
            var p =
                new WindowsPrincipal(WindowsIdentity.GetCurrent());

            Thread.CurrentPrincipal = FederatedAuthentication.FederationConfiguration.IdentityConfiguration
                .ClaimsAuthenticationManager.Authenticate("none",p) as IPrincipal;
        }

        internal void UsePrincipal()
        {
            var p = Thread.CurrentPrincipal;
            Console.WriteLine(p.Identity.Name);
        }
    }
}
