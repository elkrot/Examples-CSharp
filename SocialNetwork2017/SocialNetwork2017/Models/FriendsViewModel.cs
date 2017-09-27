using SocialNetwork2017.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork2017.Models
{
    public class FriendsViewModel
    {
        //Друзья
        public IEnumerable<User> Friends { get; set; }

        //Входящие заявки в друзья
        public IEnumerable<User> IncomingRequests { get; set; }

        //Исходящие заявки в друзья
        public IEnumerable<User> OutgoingRequests { get; set; }
    }
}