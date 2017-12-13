using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDAP
{
    public class SDS2
    {// Оптимизация

        public void Find() {
            DirectorySearcher searcher = new DirectorySearcher();
            searcher.SearchRoot = new DirectoryEntry("LDAP://OU=Sample,DC=Demo,DC=local");
            searcher.Filter = "(sAMAccountName=user.name)";
            SearchResultCollection results = searcher.FindAll();
            if (results.Count == 1)
            {
                Guid userGuid;
                DirectoryEntry user = results[0].GetDirectoryEntry();
                userGuid = user.Guid;
                user = null;
                try
                {
                    object username = user.Properties["sAMAccountName"].Value;
                }
                catch (Exception)
                {

                    throw;
                }

                user = new DirectoryEntry(string.Format("LDAP://<GUID={0}>", userGuid.ToString("D")));
                using (user)
                {
                    object username = user.Properties["sAMAccountName"].Value;
                }

            }
            else {

            }
        }





        public void GC() {

            DirectoryEntry gc = new DirectoryEntry("GC:");

            foreach (DirectoryEntry root in gc.Children) {
                gc = root;
            }
            DirectorySearcher searcher = new DirectorySearcher();
            searcher.SearchRoot = gc;
            searcher.Filter = "(sAMAccountNsme=user.name)";
            SearchResultCollection results = searcher.FindAll();

            if (results.Count == 2) {
                string path = results[0].Properties["distinguishedName"][0].ToString();

                DirectoryEntry dc = new DirectoryEntry();
                dc.Path = string.Format("LDAT://{0}", path);

                string username = dc.Properties["sAMAccountName"].Value.ToString();


            }


        }




        public void ASQ() {

            DirectoryEntry group = new DirectoryEntry("LDAP://CN=Talent,OU=...,DC=...,DC-local");

            DirectorySearcher searcher = new DirectorySearcher();
            searcher.SearchRoot = group;

            searcher.Filter = "(&(objectClass=contact)(objectCategory=person)(mail=*))";
            searcher.PropertiesToLoad.Add("mail");
            searcher.SearchScope = SearchScope.Base;
            searcher.AttributeScopeQuery = "member";

            using (SearchResultCollection results = searcher.FindAll())
            {

                List<string> mail = new List<string>();
                foreach (SearchResult result in results)
                {
                    mail.Add(result.Properties["mail"][0].ToString());
                }

                int count = mail.Count();
            }
        }



        public void Connections() {

            object tmp;
            DirectoryEntry connection1 = new DirectoryEntry("LDAP://DC=demo,DC=local"); ;

            using (connection1) {
                tmp = connection1.NativeObject;

            }


            DirectoryEntry connection2 = new DirectoryEntry("LDAP://OU=Sample,DC=demo,DC=local");

                using (connection2) {
                tmp = connection2.NativeObject;
                DirectoryEntry connection3 = new DirectoryEntry("://CN=Users,DC=demo,DC=local");
                tmp = connection3.NativeObject;

                DirectoryEntry connection4 = new DirectoryEntry("://CN=Users,DC=demo,DC=local", null, null, AuthenticationTypes.FastBind | AuthenticationTypes.Secure);

                tmp = connection4.NativeObject;
            }
        }


    }
}
