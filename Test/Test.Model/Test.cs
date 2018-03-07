using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test.Model
{
    public class TestEntity
    {
        [Key]
        public int TestKey { get; set; }
        [Required]
        [StringLength(50)]
        public string TestTitle { get; set; }

        public int? FavoriteLanguageId { get; set; }
        public ProgrammingLanguage FavoriteLanguage { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
