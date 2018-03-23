using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.WindowsAuth
{
    public class ClaimsAuthorization
    {
        internal void SetPrincipal()
        {
            var p = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            Thread.CurrentPrincipal =
                FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager.Authenticate("none", p) as IPrincipal;
        }

        internal void UsePrincipal()
        {
            ShowCastle();
        }

        [ClaimsPrincipalPermission(SecurityAction.Demand,
            Operation ="Show",
            Resource ="Castle")]
        public void ShowCastle() {
            Console.WriteLine("Wonderful");
            //ClaimsPrincipalPermission.CheckAccess()
            //var authZ = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthorizationManager;
            //authZ.CheckAccess();
        }
    }
}
