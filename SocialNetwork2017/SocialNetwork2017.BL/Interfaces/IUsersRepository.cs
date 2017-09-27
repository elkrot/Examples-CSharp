using SocialNetwork2017.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace SocialNetwork2017.BL.Interfaces
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int Id);
        User GetUserByName(string name);
        MembershipUser GetMembershipUserByName(string name);
        string GetUserNameByEmail(string email);
        void CreateUser(string userName, string password, string email, string firstName, string lastName, string middleName);
        bool ValidateUser(string userName, string password);
        void SaveUser(User user);


    }
}
