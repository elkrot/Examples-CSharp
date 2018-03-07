using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }

        public int TestKey { get; set; }
        public TestEntity Test { get; set; }

    }
}
