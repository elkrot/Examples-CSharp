using System.DirectoryServices.AccountManagement;

namespace LDAP
{
    internal class UserExtended:UserPrincipal
    {
        public UserExtended(PrincipalContext context):base(context)
        {
        }

        public UserExtended(
            PrincipalContext context
            ,string samAccountName
            ,string password
            , bool enabled)
            :base( context
                 , samAccountName
                 , password
                 , enabled) { }

        [DirectoryProperty("adminDescription")]
        public string AdminDescription {
            get {
                object[] extensions = this.ExtensionGet("adminDescription");
                if (extensions != null) { return (string)extensions[0]; }else { return null; }
            }

            set {
                this.ExtensionSet("adminDescription",value);
            } }
    }
}