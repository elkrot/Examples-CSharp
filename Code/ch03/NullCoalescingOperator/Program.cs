using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullCoalescingOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            int? n = null;
            object obj = "Hello";
            int x = 13;
            
            //short for if (n!=null) o = n; else o = -1;
            int? o = n ?? -1;
            object obj2 = obj ?? "ok"; 

            //doesn't make sense since x can't be null
            //int y = x ?? -1;
                        
            Console.WriteLine("o = {0}", o);
            Console.WriteLine("obj2 = {0}", obj2);

            

            Console.ReadKey();
        }
    }
}
