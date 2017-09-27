using SocialNetwork2017.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2017.Models
{
    public class OutgoingMessageViewModel
    {
        //Исходящее сообщение
        public OutgoingMessage Message { get; set; }

        //Адресат
        public User UserTo { get; set; }
    }
}