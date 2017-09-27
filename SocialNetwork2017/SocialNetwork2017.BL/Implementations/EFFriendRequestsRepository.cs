using SocialNetwork2017.BL.Interfaces;
using SocialNetwork2017.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SocialNetwork2017.BL.Implementations
{
    public class EFFriendRequestsRepository : IFriendRequestsRepository
    {
        private SocialNetworkContext context;
        public EFFriendRequestsRepository(SocialNetworkContext context)
        {
            this.context = context;
        }

        public IEnumerable<FriendRequest> GetFriendRequests()
        {
            return context.FriendRequests;
        }

        public bool RequestIsSent(int userFromId, int userToId)
        {
            return context.FriendRequests.Count(x => x.UserId == userFromId && x.PossibleFriendId == userToId) != 0;
        }

        public void AddFriendRequest(FriendRequest friendRequest)
        {
            context.FriendRequests.Add(friendRequest);
            context.SaveChanges();
        }

        public void DeleteFriendRequest(FriendRequest friendRequest)
        {
            context.FriendRequests.Remove(friendRequest);
            context.SaveChanges();
        }
    }
}
