using SocialNetwork2017.BL;
using SocialNetwork2017.Domain;
using SocialNetwork2017.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SocialNetwork2017.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private DataManager dataManager;
        public FriendsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index()
        {
            int currentUserId = (int)Membership.GetUser().ProviderUserKey;

            FriendsViewModel model = new FriendsViewModel();

            //Выбираем всех пользователей
            IEnumerable<User> allUsers = dataManager.Users.GetUsers();

            //Выбираем все заявки в друзья (входящие и исходящие) для пользователя, кто сейчас залогинен на сайте
            IEnumerable<FriendRequest> allRequests =
                dataManager.FriendRequests.GetFriendRequests().Where(
                    x =>
                    x.UserId == currentUserId ||
                    x.PossibleFriendId == currentUserId);

            //Из всех заявок отдельно выбираем входящие
            IEnumerable<FriendRequest> incomingRequests = allRequests.Where(x => x.UserId == currentUserId);

            //И исходящие
            IEnumerable<FriendRequest> outgoingRequests = allRequests.Where(x => x.PossibleFriendId == currentUserId);

            //По Id из входящих заявок выбираем соответствующих User'ов и передаем их в модель
            model.IncomingRequests = (from aU in allUsers
                                      from iR in incomingRequests
                                      where aU.Id == iR.PossibleFriendId
                                      select aU);

            //То же самое делаем с исходящими заявками
            model.OutgoingRequests = (from aU in allUsers
                                      from oR in outgoingRequests
                                      where aU.Id == oR.UserId
                                      select aU);

            //Выбираем все записи о друзьях для пользователя, кто сейчас залогинен на сайте
            //Обратите внимание, что мы проверяем только поле FriendId (UserId не трогаем), 
            //так как в методе ConfirmFriendRequest создается дублирующая запись,
            //меняющая id пользователя и его друга местами
            IEnumerable<Friend> allFriends = dataManager.Friends.GetFriends().Where(x => x.FriendId == currentUserId);

            //По Id из записей о друзьях выбираем соответствующих User'ов и передаем их в модель
            model.Friends = (from aU in allUsers
                             from aF in allFriends
                             where aU.Id == aF.UserId
                             select aU);

            return View(model);
        }

        //Пользователь подает заявку в друзья другому пользователю
        public ActionResult AddFriendRequest(int id)
        {
            FriendRequest friendRequest = new FriendRequest
            {
                UserId = id,
                PossibleFriendId = (int)Membership.GetUser().ProviderUserKey
            };
            dataManager.FriendRequests.AddFriendRequest(friendRequest);

            return RedirectToAction("Index", "Home", new { id });
        }

        //Пользователь решил отменить свою заявку в друзья
        public ActionResult CancelFriendRequest(int id)
        {
            dataManager.FriendRequests.DeleteFriendRequest(
                dataManager.FriendRequests.GetFriendRequests().FirstOrDefault(
                    x => x.UserId == id && x.PossibleFriendId == (int)Membership.GetUser().ProviderUserKey));

            return RedirectToAction("Index", "Home", new { id });
        }

        //Пользователь решил отклонить заявку на дружбу от другого пользователя
        public ActionResult DisagreeFriendRequest(int id)
        {
            dataManager.FriendRequests.DeleteFriendRequest(
                dataManager.FriendRequests.GetFriendRequests().FirstOrDefault(
                    x => x.UserId == (int)Membership.GetUser().ProviderUserKey && x.PossibleFriendId == id));

            return RedirectToAction("Index", "Home", new { id });
        }

        //Пользователь подтвердил заявку в друзья от другого пользователя
        public ActionResult ConfirmFriendRequest(int id)
        {
            //Специально создаем дублирующую запись в отношениях "Друзья" для обоих пользователей
            Friend friend1 = new Friend { UserId = (int)Membership.GetUser().ProviderUserKey, FriendId = id };
            Friend friend2 = new Friend { UserId = id, FriendId = (int)Membership.GetUser().ProviderUserKey };
            dataManager.Friends.AddFriend(friend1);
            dataManager.Friends.AddFriend(friend2);

            //Удаляем соответствующую заявку в друзья
            dataManager.FriendRequests.DeleteFriendRequest(dataManager.FriendRequests.GetFriendRequests().FirstOrDefault
                                                               (
                                                                   x =>
                                                                   (x.UserId == id &&
                                                                    x.PossibleFriendId ==
                                                                    (int)Membership.GetUser().ProviderUserKey) ||
                                                                   (x.UserId ==
                                                                    (int)Membership.GetUser().ProviderUserKey &&
                                                                    x.PossibleFriendId == id)));

            return RedirectToAction("Index", "Home", new { id });
        }

        //Пользователь удалил другого пользователя из друзей
        public ActionResult DeleteFriend(int id)
        {
            //Так как при добавлении записи о друзьях мы создавали дублирующую запись с переменой Id пользователей
            //теперь необходимо удалить обе эти записи, чтобы сохранялась целостность данных
            dataManager.Friends.DeleteFriend(
                dataManager.Friends.GetFriends().FirstOrDefault(
                    x => x.UserId == id && x.FriendId == (int)Membership.GetUser().ProviderUserKey));
            dataManager.Friends.DeleteFriend(
                dataManager.Friends.GetFriends().FirstOrDefault(
                    x => x.UserId == (int)Membership.GetUser().ProviderUserKey && x.FriendId == id));

            return RedirectToAction("Index", "Home", new { id });
        }
    }
}