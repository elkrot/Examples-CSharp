using System;
using System.ServiceModel;
using System.IO;

namespace FileServiceLib
{
    public class FileService : IFileService
    {
        //just because it's a file service doesn't mean that you have to 
        //actually use the real underlying file system--you could make 
        //your own virtual file system

        public string[] GetSubDirectories(string directory)
        {
            return System.IO.Directory.GetDirectories(directory);
        }

        public string[] GetFiles(string directory)
        {
            return System.IO.Directory.GetFiles(directory);
        }

        public int RetrieveFile(string filename, int amountToRead, out byte[] bytes)
        {
            bytes = new byte[amountToRead];
            using (FileStream stream = File.OpenRead(filename))
            {
                return stream.Read(bytes, 0, amountToRead);
            }
        }

    }
}
