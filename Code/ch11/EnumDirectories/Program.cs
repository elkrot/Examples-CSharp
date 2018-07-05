using System;
using System.Collections.Generic;
using System.IO;

namespace EnumDirectories
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: EnumDirectories RootDirectory");
                return;
            }

            string root = args[0];

            //old way--get all strings up front
            DirectoryInfo di = new DirectoryInfo(root);
            DirectoryInfo[] directories = di.GetDirectories("*", SearchOption.AllDirectories);
            
            //new way--use an enumerator
            di = new DirectoryInfo(root);
            IEnumerable<DirectoryInfo> dirInfo =  di.EnumerateDirectories("*", SearchOption.AllDirectories);
            foreach (DirectoryInfo info in dirInfo)
            {
                Console.WriteLine(info.Name);
            }

        }
    }
}
