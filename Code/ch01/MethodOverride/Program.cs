using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MethodOverride
{
    class Base
    {
        public virtual void DoSomethingVirtual()
        {
            Console.WriteLine("Base.DoSomethingVirtual");
        }

        public void DoSomethingNonVirtual()
        {
            Console.WriteLine("Base.DoSomethingNonVirtual");
        }
    }
    class Derived : Base
    {
        public override void DoSomethingVirtual()
        {
            Console.WriteLine("Derived.DoSomethingVirtual");
        }
        public new void DoSomethingNonVirtual()
        {
            Console.WriteLine("Derived.DoSomethingNonVirtual");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Derived via Base reference:");
            
            Base baseRef = new Derived();
            baseRef.DoSomethingVirtual();
            baseRef.DoSomethingNonVirtual();
            
            Console.WriteLine();
            Console.WriteLine("Derived via Derived reference:");

            Derived derivedRef = new Derived();
            derivedRef.DoSomethingVirtual();
            derivedRef.DoSomethingNonVirtual();

            Console.ReadKey();
        }
    }
}
