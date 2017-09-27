using SocialNetwork2017.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2017.Models
{
    public class IncomingMessageViewModel
    {
        //Входящее сообщение
        public IncomingMessage Message { get; set; }

        //Автор
        public User UserFrom { get; set; }
    }
}