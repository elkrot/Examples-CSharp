namespace SocialNetwork2017.Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SocialNetworkContext : DbContext
    {
        public SocialNetworkContext()
            : base("name=SocialNetwork2017")
        {
        }

        public virtual DbSet<FriendRequest> FriendRequests { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<IncomingMessage> IncomingMessages { get; set; }
        public virtual DbSet<OutgoingMessage> OutgoingMessages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
