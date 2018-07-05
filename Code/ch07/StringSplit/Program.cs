using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringSplit
{
    class Program
    {
        static void Main(string[] args)
        {
            string original =
                "But, in a larger sense, we can not dedicate—we can not consecrate—we can not hallow—this ground.";
            char[] delims = new char[]{',','-',' ','.'};
            string[] strings = original.Split(delims);
            Console.WriteLine("Default split behavior:");
            foreach (string s in strings)
            {
                Console.WriteLine("\t{0}", s);
            }
            Console.WriteLine();
            strings = original.Split(delims, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("StringSplitOptions.RemoveEmptyEntries:");
            foreach (string s in strings)
            {
                Console.WriteLine("\t{0}", s);
            }
            Console.ReadKey();
        }
    }
}
