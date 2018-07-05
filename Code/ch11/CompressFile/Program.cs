using System;
using System.IO;
using System.IO.Compression;

namespace CompressFile
{
    class Program
    {
        static bool compress = false;
        static string sourceFile = null;
        static string destFile = null;
        const int BufferSize = 16384;

        static void Main(string[] args)
        {
            if (!ParseArgs(args))
            {
                Console.WriteLine("CompressFile [compress|decompress] sourceFile destFile");
                return;
            }

            byte[] buffer = new byte[BufferSize];
            /* First create regular streams to both source and destination files.
             * Then create a gzip stream that encapsulates one or the other,
             * depending on if we need to compress or uncompress.
             * i.e. If we hook up the gzip stream to the output file stream and then write bytes
             * to it, those bytes will be compressed.
             */
            using (Stream inFileStream = File.Open(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream outFileStream = File.Open(destFile, FileMode.Create, FileAccess.Write, FileShare.None))
            using (GZipStream gzipStream = new GZipStream(
                compress ? outFileStream : inFileStream,
                compress ? CompressionMode.Compress : CompressionMode.Decompress))
            {
                Stream inStream = compress?inFileStream:gzipStream;
                Stream outStream = compress?gzipStream:outFileStream;
                
                int bytesRead = 0;
                do
                {
                    bytesRead = inStream.Read(buffer, 0, BufferSize);
                    outStream.Write(buffer, 0, bytesRead);
                } while (bytesRead > 0);
            }
        }

        private static bool ParseArgs(string[] args)
        {
            if (args.Length < 3)
                return false;

            if (string.Compare(args[0], "compress") == 0)
                compress = true;
            else if (string.Compare(args[0], "decompress") == 0)
                compress = false;
            else return false;
            sourceFile = args[1];
            destFile = args[2];
            return true;
        }
    }
}
