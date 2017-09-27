using System.Collections.Generic;
using System.Linq;
using SocialNetwork2017.BL.Interfaces;
using SocialNetwork2017.Domain;

namespace SocialNetwork2017.BL.Implementations
{
    public class EFFriendsRepository : IFriendsRepository
    {
        private SocialNetworkContext context;
        public EFFriendsRepository(SocialNetworkContext context)
        {
            this.context = context;
        }

        public IEnumerable<Friend> GetFriends()
        {
            return context.Friends;
        }

        public bool UsersAreFriends(int userId, int user2Id)
        {
            return context.Friends.Count(x => x.UserId == userId && x.FriendId == user2Id) != 0;
        }

        public void AddFriend(Friend friend)
        {
            context.Friends.Add(friend);
            context.SaveChanges();
        }

        public void DeleteFriend(Friend friend)
        {
            context.Friends.Remove(friend);
            context.SaveChanges();
        }
    }
}
