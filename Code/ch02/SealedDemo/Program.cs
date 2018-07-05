using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SealedDemo
{
    class ParentClass
    {
        public virtual void MyFunc()
        {
        }
    }

    class ChildClass : ParentClass
    {
        public sealed override void MyFunc()
        {
            
        }
    }

    class GrandChildClass : ChildClass
    {
        //yields compile error
        public override void MyFunc()
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
