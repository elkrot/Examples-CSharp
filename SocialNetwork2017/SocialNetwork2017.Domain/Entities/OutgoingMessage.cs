namespace SocialNetwork2017.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OutgoingMessage
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int UserToId { get; set; }

        [Required(ErrorMessage ="*")]
        [StringLength(1024)]
        public string Text { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }
    }
}
