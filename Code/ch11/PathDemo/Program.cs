using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PathDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows\System32\xcopy.exe";
            Console.WriteLine(path);
            Console.WriteLine("GetDirectoryName: {0}", Path.GetDirectoryName(path));
            Console.WriteLine("Extension: {0}", Path.GetExtension(path));
            Console.WriteLine("FileName: {0}", Path.GetFileName(path));
            Console.WriteLine("FileNameWithoutExtension: {0}", Path.GetFileNameWithoutExtension(path));
            Console.WriteLine("FullPath: {0}",Path.GetFullPath(Path.GetFileName(path)));
            Console.WriteLine("Root: {0}", Path.GetPathRoot(path));
            Console.WriteLine("HasExtension: {0}", Path.HasExtension(path));
            Console.WriteLine("{0} IsPathRooted: {1}", path, Path.IsPathRooted(path));
            Console.WriteLine("{0} IsPathRooted: {1}", Path.GetFileName(path), Path.IsPathRooted(Path.GetFileName(path)));

            Console.WriteLine("RandomFileName: {0}", Path.GetRandomFileName());
            Console.WriteLine("TempFileName: {0}", Path.GetTempFileName());
            Console.WriteLine("TempPath: {0}", Path.GetTempPath());
            
            Console.WriteLine("InvalidFileNameChars: {0}", Path.GetInvalidFileNameChars());
            Console.WriteLine("InvalidPathChars: {0}", Path.GetInvalidPathChars());
            Console.WriteLine("AltDirectorySeparatorChar: {0}", Path.AltDirectorySeparatorChar);
            Console.WriteLine("DirectorySeparatorChar: {0}", Path.DirectorySeparatorChar);
            Console.WriteLine("PathSeparatorChar: {0}", Path.PathSeparator);
            Console.WriteLine("VolumeSeparatorChar: {0}", Path.VolumeSeparatorChar);

            Console.WriteLine("Combine \"C:\\Windows\\System32\" and \"xcopy.exe\": {0}", Path.Combine(@"C:\windows\system32", "xcopy.exe"));
            Console.WriteLine("Change ext: {0}", Path.ChangeExtension(path, ".bin"));

            Console.ReadKey();
        }
    }
}
