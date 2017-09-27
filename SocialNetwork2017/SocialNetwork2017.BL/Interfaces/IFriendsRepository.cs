using SocialNetwork2017.Domain;
using System.Collections.Generic;

namespace SocialNetwork2017.BL.Interfaces
{
    public interface IFriendsRepository
    {
        IEnumerable<Friend> GetFriends();
        //Проверяем, являются ли два пользователя друзьями
        bool UsersAreFriends(int userId, int user2Id);
        void AddFriend(Friend friend);
        void DeleteFriend(Friend friend);
    }
}
