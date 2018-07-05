using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace ShowFileSecurity
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: ShowFileSecurity filename");
                return;
            }
            string filename = args[0];
            FileInfo info = new FileInfo(filename);
            FileSecurity security = info.GetAccessControl();
            ShowSecurity(security);
        }

        private static void ShowSecurity(FileSecurity security)
        {
            AuthorizationRuleCollection coll = security.GetAccessRules(true, true, typeof(NTAccount));
            foreach (FileSystemAccessRule rule in coll)
            {
                Console.WriteLine("IdentityReference: {0}", rule.IdentityReference);
                Console.WriteLine("Access control type: {0}", rule.AccessControlType);
                Console.WriteLine("Rights: {0}", rule.FileSystemRights);
                Console.WriteLine("Inherited? {0}", rule.IsInherited);
                
                Console.WriteLine();
            }
        }
    }
}
