namespace SocialNetwork2017.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FriendRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PossibleFriendId { get; set; }
    }
}
