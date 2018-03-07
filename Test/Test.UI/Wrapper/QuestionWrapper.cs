using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model;

namespace Test.UI.Wrapper
{
    public class QuestionWrapper : ModelWrapper<Question>
    {
        public QuestionWrapper(Question model) : base(model)
        {
        }
        

            public string QuestionTitle
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


    }
}
