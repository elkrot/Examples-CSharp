using System.Collections.Generic;

using SocialNetwork2017.Domain;

namespace SocialNetwork2017.BL.Interfaces
{
    public interface IFriendRequestsRepository
    {
        IEnumerable<FriendRequest> GetFriendRequests();
        //Проверяем, отправил ли пользователь1 пользователю2 запрос на дружбу
        bool RequestIsSent(int userFromId, int userToId);
        void AddFriendRequest(FriendRequest friendRequest);
        void DeleteFriendRequest(FriendRequest friendRequest);
    }
}
