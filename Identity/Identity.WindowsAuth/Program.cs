using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.WindowsAuth
{
    class Program
    {
        static void Main(string[] args)
        {
            // WindowsIdentity();

            //  GenerisPrincipal();

            // Claim();

            // Inheritance();

            //var ct = new ClaimsTransformation();
            //ct.SetPrincipal();
            //ct.UsePrincipal();

            var ca = new ClaimsAuthorization();
            ca.SetPrincipal();
            ca.UsePrincipal();


            Console.Read();
        }

        private static void Inheritance()
        {
            var id = WindowsIdentity.GetCurrent();
        }

        private static void Claim()
        {
            SetupClaimPrincipal();
            UserPrincipalLegacy();

            UserPrincipalNewCode();

        }

        private static void UserPrincipalNewCode()
        {
            //var p = Thread.CurrentPrincipal;
            //var cp = p as ClaimsPrincipal;

            var cp = ClaimsPrincipal.Current;

            var email = cp.FindFirst(ClaimTypes.Email).Value;
            Console.WriteLine(email);

        }

        private static void UserPrincipalLegacy()
        {
            var p = Thread.CurrentPrincipal;
            Console.WriteLine(p.Identity.Name);
            Console.WriteLine(p.IsInRole("Admin"));
        }

        private static void SetupClaimPrincipal()
        {
            var claim = new Claim(ClaimTypes.Name, "serg");

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, "serg")
                ,new Claim(ClaimTypes.Email, "serg@gmail.com")
                ,new Claim(ClaimTypes.Role, "Admin")
        };
            var id = new ClaimsIdentity(claims, "Console App",ClaimTypes.Name,ClaimTypes.Role);
            Console.WriteLine(id.IsAuthenticated);

            var p = new ClaimsPrincipal(id);

            Thread.CurrentPrincipal = p;
        }

        private static void GenerisPrincipal()
        {
            SetupPrincipal();
            UserPrincipal();
        }

        private static void UserPrincipal()
        {

            var p = Thread.CurrentPrincipal;
            Console.WriteLine(p.Identity.Name);
            Console.WriteLine(p.IsInRole("Mark"));

            if (p.IsInRole("Mark")) {

            }

            new PrincipalPermission(null, "Mark").Demand();

            DoDeveloperWork();

        }

        [PrincipalPermission(SecurityAction.Demand,Role="Dev")]
        private static void DoDeveloperWork()
        {
            Console.WriteLine("You dev");
        }
       
        private static void SetupPrincipal()
        {
            // autenticate client

            var id = new GenericIdentity("bob");

            var roles = new string[] { "Dev", "Mark" };

            var p = new GenericPrincipal(id, roles);

            Thread.CurrentPrincipal = p;
        }

        private static void WindowsIdentityTest()
        {
            var id = System.Security.Principal.WindowsIdentity.GetCurrent();
            Console.WriteLine(id.Name);


            var account = new NTAccount(id.Name);
            var sid = account.Translate(typeof(SecurityIdentifier));
            Console.WriteLine(sid.Value);

            foreach (var item in id.Groups.Translate(typeof(NTAccount)))
            {
                Console.WriteLine(item);
            }
            var principal = new WindowsPrincipal(id);

            var localAdmins = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

            Console.WriteLine(principal.IsInRole(localAdmins));

            var domainAdmins = new SecurityIdentifier(WellKnownSidType.AccountDomainAdminsSid, id.User.AccountDomainSid);

            Console.WriteLine(principal.IsInRole(domainAdmins));

            var user = new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null);

            Console.WriteLine(principal.IsInRole(user));
        }
    }

    class CorpIdentity : ClaimsIdentity {
        public CorpIdentity(string name,string repTo)
        {
            AddClaim(new Claim(ClaimTypes.Name, name));
            AddClaim(new Claim("reportsto", repTo));
        }

        public string ReportsTo { get { return FindFirst("reportsTo").Value; }  }
    }
}
