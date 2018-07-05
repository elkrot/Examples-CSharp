using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttributeDemo
{
    [Culture("en-US")]
    [Culture("en-GB")]
    class Program
    {
        static void Main(string[] args)
        {
            CultureAttribute[] attributes = (CultureAttribute[])(typeof(Program)).GetCustomAttributes(typeof(CultureAttribute), true);
            //easy comma-separated list
            string list = attributes.Aggregate("", (output, next) => (output.Length > 0) ? (output + ", " + next.Culture) : next.Culture);
            Console.WriteLine("Cultures of Program: {0}", list);

            Console.ReadKey();            
        }
    }
}
