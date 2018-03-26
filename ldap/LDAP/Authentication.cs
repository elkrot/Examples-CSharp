using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace LDAP
{
    public class Authentication
    {
        public bool Authenticate(string UserName, string Password) {
            const int ERROR_LOGON_FAILURE = -2147023570;

            DirectoryEntry root = new DirectoryEntry(
                "LDAP://rootDSE",
                UserName,
                Password,
                AuthenticationTypes.Secure
                );

            using (root)
            {
                try
                {
                    object tmp = root.NativeObject;
                    return true;
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    if (ex.ErrorCode!= ERROR_LOGON_FAILURE)
                    throw;
                    return false;
                }
            }

        }


        public bool Authenticate2(string UserName, string Password) {

            PrincipalContext adContext = new PrincipalContext(ContextType.Domain);
            using (adContext)
            {
                return adContext.ValidateCredentials(UserName, Password);
            }
        }


        public void ASP_NET_PageLoad(object sender, EventArgs e) {
            PrincipalContext context = new PrincipalContext(ContextType.Domain);

            string username= HttpContent.Current.User.Identity.Name;
            UserPrincipal user = UserPrincipal.FindByIdentity(context, username);
            lblPhone.Text = user.VoiceTelephoneNumber;
        }

    }
}
/*
 Web.config

    <connectionSrings>
    <add name ="ADService" connectionString = "LDAP://demo.local"/>
    ...

    <authentication mode ="Forms">
    <forms loginUrl = "~/Login.aspx" timeout="2880"/>
    
    <membership defaultProvider ="AspNetActiveDirectoryMembershipProvider">
    <clear/>
    <add name="AspNetActiveDirectoryMembershipProvider"
    type = "System.Web.Security.ActiveDirectoryMembershipProvider,System.Web"
    connectionStringName ="ADService"
    attributeMapUsername="sAMAccountName"/>
    </providers>
    </membership>
    <authorization>
    <deny users="?"/>
    </authorization>
     
     */
