using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDAP
{
    class Program
    {
        public static DirectoryEntry SampleOU;
        const int ADS_UF_ACCOUNTDISABLE = 2;
        static void Main(string[] args)
        {
            var Test = new DirectoryEntry("LDAP://ldap.itd.umich.edu", null, null, AuthenticationTypes.Anonymous);
            DirectoryEntry RoutOU = new DirectoryEntry(
                "LDAP://DC=demo,dc=local",
                null, //username
                null,//password
                AuthenticationTypes.Secure
                );

            SampleOU = new DirectoryEntry(
               "LDAP://OU=sample,DC=demo,dc=local",
               null, //username
               null,//password
               AuthenticationTypes.Secure
               );
            DirectoryEntry groupEnt = SampleOU.Children.Add("CN=TestGroup", "Group");
            using (groupEnt)
            {
                groupEnt.Properties["sAMAccountName"].Value = "TestGroup";
                groupEnt.CommitChanges();
            }
        }

        #region CreateUsers
        public void CreateUsers()
        {
            List<Users> UserInfo = UserTools.Collect();
            foreach (Users user in UserInfo)
            {
                DirectoryEntry userEnt =
                    SampleOU.Children.Add(string.Format("CN={0}", user.UserName), "User");
                using (userEnt)
                {
                    userEnt.Properties["sAMAccountName"].Value = user.UserName;
                    userEnt.Properties["userPrincipalName"].Value = user.UserName + "@demo.local";
                    userEnt.Properties["givenName"].Value = user.LastName;
                    userEnt.Properties["sn"].Value = user.LastName;
                    userEnt.CommitChanges();

                    int val = (int)userEnt.Properties["sAMAccountName"].Value;
                    userEnt.Properties["userAccountControl"].Value =
                        val & ~ADS_UF_ACCOUNTDISABLE;
                    userEnt.CommitChanges();
                }
            }
        }

        #endregion
        DirectoryEntry user1;
        DirectoryEntry group1;
        #region FindUser
        public static void FindUser()
        {
            DirectorySearcher searcher = new DirectorySearcher();
            SearchResultCollection results;
            searcher.Filter = "(sAVFccountName=ivan.ivanov)";
            //(&(...)(sn=*e))
            searcher.SearchRoot = SampleOU;
            DirectoryEntry user1;
            using (searcher)
            {
                results = searcher.FindAll();
                if (results.Count == 1)
                {
                    string path = results[0].Path;
                    user1 = results[0].GetDirectoryEntry();
                }
                else if (results.Count == 0)
                {
                    //NOtFound
                }
                else
                {
                    // error
                }
            }
            #endregion

            #region RWAttribute
        public void RWAttribute()
        {
            user1.Properties["Description"].Value = "ccc";
            user1.CommitChanges();
            string Password = "";
            user1.Invoke("SetPassword", new { Password });
            SearchResult result;
            result.GetDirectoryEntry().DeleteTree();
        }
        #endregion

        #region GroupMembership
        public void GroupMembership()
        {
            string UserDn = user1.Properties["distinguishedName"].Value.ToString();
            group1.Properties["member"].Add(UserDn);
            group1.CommitChanges();
        }
        #endregion


        public void xxx() {
            PrincipalContext DirectoryContext = new PrincipalContext(ContextType.ApplicationDirectory);
            DirectoryContext = new PrincipalContext(ContextType.Domain,null,"OU=,DC=,DC=");
            UserPrincipal uPr = new UserPrincipal(DirectoryContext);

            uPr = UserPrincipal.FindByIdentity(DirectoryContext, "serg.kolod");

            //group GroupPrincipal.FindByIdentity(...
            //...UserPrincipal.Current
        }

        public void AdvSearch() {
            PrincipalContext DirectoryContext = new PrincipalContext(ContextType.Domain, null, "OU=,DC=,DC=");
            UserPrincipal userSearch = new UserPrincipal(DirectoryContext);
            userSearch.Surname = "(sn=*e)";
            PrincipalSearcher searcher = new PrincipalSearcher();
            searcher.QueryFilter = userSearch;
            using (searcher) {
                PrincipalSearchResult<Principal> results = searcher.FindAll();

                foreach (var item in results)
                {

                }
            }
        }


        public void ConvertDE() {
            PrincipalContext DirectoryContext = new PrincipalContext(ContextType.Domain, null, "OU=,DC=,DC=");
            UserPrincipal user1 = new UserPrincipal(DirectoryContext);

            DirectoryEntry userDE = (DirectoryEntry)user1.GetUnderlyingObject();
            DirectoryEntry parent = userDE.Parent;
        }

        public void Extend() {
            PrincipalContext DirectoryContext = new PrincipalContext(ContextType.Domain, null, "OU=,DC=,DC=");
            UserExtended ue = new UserExtended(DirectoryContext);
            ue.SamAccountName = "New.user";
            ue.AdminDescription = "Admin Description";
            using (ue) {
                ue.Save();
            }

        }

        public void SetPassworg(string UserName, string Password) {
            PrincipalContext DirectoryContext = new PrincipalContext(ContextType.Domain, null, "OU=,DC=,DC=");
            UserPrincipal user = UserPrincipal.FindByIdentity(DirectoryContext,UserName);

            user.SetPassword(Password);
        }

        public void DeleteUser(string UserName)
        {
            PrincipalContext DirectoryContext = new PrincipalContext(ContextType.Domain, null, "OU=,DC=,DC=");
            UserPrincipal user = UserPrincipal.FindByIdentity(DirectoryContext, UserName);

            user.Delete();
        }












    }
}

