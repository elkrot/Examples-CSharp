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
    public class MessagesController : Controller
    {
        private DataManager dataManager;
        public MessagesController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }

        public ActionResult Index()
        {
            //Подготавливаем все входящие сообщения и авторов этих сообщений
            //и передаем их сначала в ViewModel, а далее в представление
            List<IncomingMessageViewModel> model = new List<IncomingMessageViewModel>();
            foreach (IncomingMessage message in dataManager.Messages.GetIncomingMessagesByUserId((int)Membership.GetUser().ProviderUserKey))
            {
                model.Add(new IncomingMessageViewModel
                {
                    Message = message,
                    UserFrom = dataManager.Users.GetUserById(message.UserFromId)
                });
            }
            return View(model);
        }

        public ActionResult Outgoing()
        {
            //Подготавливаем все исходящие сообщения и адресатов этих сообщений и передаем их сначала
            //в ViewModel, а потом в представление
            List<OutgoingMessageViewModel> model = new List<OutgoingMessageViewModel>();
            foreach (OutgoingMessage message in dataManager.Messages.GetOutgoingMessagesByUserId((int)Membership.GetUser().ProviderUserKey))
            {
                model.Add(new OutgoingMessageViewModel
                {
                    Message = message,
                    UserTo = dataManager.Users.GetUserById(message.UserToId)
                });
            }
            return View(model);
        }

        public ActionResult NewMessage(int userToId)
        {
            //Перед отправкой модели в представление заранее определяем автора сообщения,
            //адресата и дату создания сообщения
            return
                View(new OutgoingMessage
                {
                    UserId = (int)Membership.GetUser().ProviderUserKey,
                    UserToId = userToId,
                    CreatedDate = DateTime.Now
                });
        }
        [HttpPost]
        public ActionResult NewMessage(OutgoingMessage message)
        {
            if (ModelState.IsValid)
            {
                //После сохранения исходящего сообщения создаем соответствующее
                //входящее сообщение для другого пользователя
                dataManager.Messages.SaveOutgoingMessage(message);
                dataManager.Messages.SaveIncomingMessage(new IncomingMessage
                {
                    UserId = message.UserToId,
                    UserFromId = message.UserId,
                    CreatedDate = DateTime.Now,
                    Text = message.Text
                });
                return RedirectToAction("Index", "Home");
            }
            return View(message);
        }

    }
}
