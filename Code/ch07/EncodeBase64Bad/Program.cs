using System;
using System.IO;

namespace EncodeBase64Bad
{
    class Program
    {
        static void PrintUsage()
        {
            Console.WriteLine("Usage: EncodeBase64Bad [sourceFile]");
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                PrintUsage();
                return;
            }

            string sourceFile = args[0];
            
            if (!File.Exists(sourceFile))
            {
                PrintUsage();
                return;
            }

            byte[] bytes = File.ReadAllBytes(sourceFile);
            Console.WriteLine(Convert.ToBase64String(bytes));
            
        }
    }
}