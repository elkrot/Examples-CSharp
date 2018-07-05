using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace StringCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            //the infamous Turkish I problem
            string a = "file";
            string b = "FILE";
            bool equalInvariant = string.Compare(a, b, true, CultureInfo.InvariantCulture) == 0;
            bool equalTurkish = string.Compare(a, b, true, CultureInfo.CreateSpecificCulture("tr-TR")) == 0;

            Console.WriteLine("Are {0} and {1} equal?",a,b);
            Console.WriteLine("Invariant culture: " + (equalInvariant?"yes":"no"));
            Console.WriteLine("Turkish culture: " + (equalTurkish ? "yes" : "no"));
            
            Console.ReadKey();
        }
    }
}
