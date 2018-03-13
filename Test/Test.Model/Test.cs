using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Test.Model;

namespace Test.Model
{
    public class TestEntity
    {
        public TestEntity()
        {
            Questions = new Collection<Question>();
            Meetings = new Collection<Meeting>();
        }
        [Key]
        public int TestKey { get; set; }
        [Required]
        [StringLength(50)]
        public string TestTitle { get; set; }

        public int? FavoriteLanguageId { get; set; }
        public ProgrammingLanguage FavoriteLanguage { get; set; }
        public ICollection<Question> Questions { get; set; }

        public ICollection<Meeting> Meetings { get; set; }

    }
}
