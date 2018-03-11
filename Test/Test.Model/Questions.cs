using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string QuestionTitle { get; set; }

        public int TestKey { get; set; }
        public TestEntity Test { get; set; }

    }
}
