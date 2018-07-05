using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OsVersions
{
    class Program
    {
        static void Main(string[] args)
        {
            OperatingSystem os = System.Environment.OSVersion;
            Console.WriteLine("Platform: {0}", os.Platform);
            Console.WriteLine("Service Pack: {0}", os.ServicePack);
            Console.WriteLine("Version: {0}", os.Version);
            Console.WriteLine("VersionString: {0}", os.VersionString);
            Console.WriteLine("CLR Version: {0}", System.Environment.Version);
            
            Console.ReadKey();
        }
    }
}
