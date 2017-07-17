using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Crypto
{
    class Program
    {
        static void Main(string[] args)
        {
            //encrypt
            byte[] key = { 11, 22, 33, 44, 55, 66, 77, 88, 99, 100, 200, 123, 156, 34, 89, 93 };
            byte[] iv = { 34, 24, 32, 44, 55, 60, 13, 9, 22, 55, 77, 90, 23, 12, 13, 11 };
            byte[] data = Encoding.UTF8.GetBytes("this is a text to encrypt");

            using (SymmetricAlgorithm algorithm = Aes.Create())
            using (ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv))
            using (FileStream stream = new FileStream(@"c:\files\text.txt", FileMode.CreateNew))
            using (CryptoStream crypto = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
                crypto.Write(data, 0, data.Length);


            //decrypt
            StreamReader sr = null;
            using (SymmetricAlgorithm algorithm = Aes.Create())
            using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv))
            using (FileStream stream = new FileStream(@"c:\files\text.txt", FileMode.Open))
            using (CryptoStream crypto = new CryptoStream(stream, decryptor, CryptoStreamMode.Read))
            {
                sr = new StreamReader(crypto);
                Console.WriteLine(sr.ReadToEnd());
            }

            Console.ReadLine();

            FileStream fs = new FileStream(@"c:\files\text.txt", FileMode.Open);
            BufferedStream bs = new BufferedStream(new GZipStream(fs, CompressionMode.Compress));
        }

        static byte[] ReadBytes(Stream s)
        {
            byte[] dataToRead = new byte[s.Length];
            int totalBytesRead = 0;
            int chunkBytesRead = 1;
            while (totalBytesRead < dataToRead.Length && chunkBytesRead > 0)
            {
                chunkBytesRead = s.Read(dataToRead, totalBytesRead, dataToRead.Length - totalBytesRead);
                totalBytesRead = totalBytesRead + chunkBytesRead;
            }

            return dataToRead;
        }
    }
}
