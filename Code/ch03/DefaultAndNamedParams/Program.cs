using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DefaultAndNamedParams
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowFolders();
            ShowFolders(@"C:\");
            //no, you can't do this
            //ShowFolders(false);

            ShowFolders(showFullPath: false, root: @"C:\Windows");

            Console.ReadKey();
        }

        static void ShowFolders(string root = @"C:\", bool showFullPath = false)
        {
            foreach (string folder in Directory.EnumerateDirectories(root))
            {
                string output = showFullPath ? folder : Path.GetFileName(folder);
                Console.WriteLine(output);
            }
        }

        //not allowed: default parameters must appear after all non-default parameters
        //static void ShowFolders(string root = @"C:\", bool showFullPath )
        //{
        //}
    }
}
