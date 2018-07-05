using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace IsolatedStorageDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run with command line arg -r to remove isolated storage for this app/user");

            //get the isolated storage for this appdomain + user
            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForDomain())
            {
                //setup directory
                if (!file.DirectoryExists("Dummy"))
                {
                    file.CreateDirectory("Dummy");
                }
                Console.WriteLine("Accesses:");
                //read and write to a file in the directory
                using (IsolatedStorageFileStream stream = file.OpenFile(@"Dummy\accesses.txt", System.IO.FileMode.OpenOrCreate))
                using (TextReader reader = new StreamReader(stream))
                using (TextWriter writer = new StreamWriter(stream))
                {
                    string line = null;
                    do
                    {
                        line = reader.ReadLine();
                        if (line != null)
                        {
                            Console.WriteLine(line);
                        }
                    } while (line != null);

                    writer.WriteLine(DateTime.Now.ToString());
                }
                if (args.Length > 0 && args[0] == "-r")
                {
                    Console.WriteLine("Removing isolated storage for this user/app-domain");
                    file.Remove();
                }
            }

            Console.ReadKey();          
        }
    }
}
