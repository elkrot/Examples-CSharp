using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TernaryOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            bool condition = true;

            int x = condition ? 13 : 14;
            Console.WriteLine("x is {0}", x);

            //you can also embed the condition in other statements
            Console.WriteLine("Condition is {0}", condition ? "TRUE" : "FALSE");

            Console.ReadKey();
        }
    }
}
