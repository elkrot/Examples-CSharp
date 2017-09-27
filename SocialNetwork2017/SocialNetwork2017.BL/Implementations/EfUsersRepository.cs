using SocialNetwork2017.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork2017.Domain;
using System.Web.Security;
using System.Data.Entity;

namespace SocialNetwork2017.BL.Implementations
{
    public class EfUsersRepository : IUsersRepository
    {
        private SocialNetworkContext context;
        public EfUsersRepository(SocialNetworkContext context)
        {
            this.context = context;
        }
        public void CreateUser(string userName, string password, string email, string firstName, string lastName, string middleName)
        {
            User user = new User()
            {
                UserName = userName,
                Email = email,
                Password = password,
                CreatedDate = DateTime.Now,
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName
            };
            SaveUser(user);
        }



        public MembershipUser GetMembershipUserByName(string name)
        {
            User user = GetUserByName(name);
            if (user != null)
            {
                return new MembershipUser(
                    "CustomMembershipProvider",
                    user.UserName,
                    user.Id,
                    user.Email,
                    "",
                    null,
                    true,
                    false,
                    user.CreatedDate,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now,
                    DateTime.Now
                    );
            }
            else return null;
        }

        public User GetUserById(int Id)
        {
            return context.Users.FirstOrDefault(x => x.Id == Id);
        }

        public User GetUserByName(string name)
        {
            return context.Users.FirstOrDefault(x => x.UserName == name);
        }

        public string GetUserNameByEmail(string email)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == email);
            return user==null?"":user.FirstName;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users;
        }

        public void SaveUser(User user)
        {
            if (user.Id == 0)
                context.Users.Add(user);
            else
            {
                context.Entry(user).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public bool ValidateUser(string userName, string password)
        {
            User user = GetUserByName(userName);
            if (user != null && user.Password == password)
                return true;
            return false;


        }



    }
}
