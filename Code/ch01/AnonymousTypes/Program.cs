using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnonymousTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            var part = new { ID = 1, Name = "Part01", Weight = 2.5 };

            Console.WriteLine("var Part, Weight: {0}", part.Weight);
            Console.WriteLine("var Part, ToString(): {0}", part.ToString());
            Console.WriteLine("var Part, Type: {0}", part.GetType());
            Console.ReadKey();
        }
    }
}
