using System;
using System.Collections.Generic;
using System.IO;

namespace Find
{
    class Program
    {
        static bool _folderOnly = false;
        static string _startFolder;
        static string _searchTerm;

        static void Main(string[] args)
        {
            if (!ParseArgs(args))
            {
                PrintUsage();
                return;
            }

            Console.WriteLine("Searching {0} for \"{1}\" {2}", 
                _startFolder, _searchTerm, _folderOnly?"(folders only)":"");

            DoSearch();
        }

        private static void DoSearch()
        {
            DirectoryInfo di = new DirectoryInfo(_startFolder);
            DirectoryInfo[] directories = di.GetDirectories(_searchTerm, SearchOption.AllDirectories);
            int numResults = directories.Length;
            PrintSearchResults(directories);
            if (!_folderOnly)
            {
                FileInfo[] files = di.GetFiles(_searchTerm,SearchOption.AllDirectories);
                PrintSearchResults(files);
                numResults += files.Length;
            }
            
            Console.WriteLine("{0:N0} results found", numResults);
        }

        private static void PrintSearchResults(DirectoryInfo[] directories)
        {
 	        foreach(DirectoryInfo di in directories)
            {
                Console.WriteLine("{0}\t{1}\t{2}",di.Name, di.Parent.FullName, "D");
            }
        }

        private static void PrintSearchResults(FileInfo[] files)
        {
 	        foreach(FileInfo fi in files)
            {
                Console.WriteLine("{0}\t{1}\t{2}",fi.Name, fi.DirectoryName, "F");
            }
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: Find [-directory] SearchTerm StartFolder");
            Console.WriteLine("Ex: Find -directory code D:\\Projects");
            Console.WriteLine("* wildcards are accepted");
            
        }

        static bool ParseArgs(string[] args)
        {
            if (args.Length < 2)
            {
                return false;
            }
            if (string.Compare(args[0], "-directory") == 0)
            {
                _folderOnly = true;
                if (args.Length < 3)
                    return false;
            }
            _startFolder = args[args.Length - 1];
            _searchTerm = args[args.Length - 2];
            return true;
        }
    }
}
