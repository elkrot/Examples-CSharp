using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Exists
{
    class Program
    {
        static void Main(string[] args)
        {
            string target = null;
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: Exists filename");
                return;
            }
            target = args[0];
            if (File.Exists(target))
            {
                Console.WriteLine("File {0} exists", target);
            }
            else if (Directory.Exists(target))
            {
                Console.WriteLine("Directory {0} exists", target);
            }
            else
            {
                Console.WriteLine("{0} does not exist", target);
            }
        }
    }
}
