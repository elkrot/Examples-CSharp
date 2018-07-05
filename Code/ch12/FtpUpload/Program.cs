using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace FtpUpload
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: FtpUpload host username password file");
                return;
            }

            string host = args[0];
            string username = args[1];
            string password = args[2];
            string file = args[3];
            if (!host.StartsWith("ftp://"))
            {
                host = "ftp://" + host;
            }
            Uri uri = new Uri(host);
            FileInfo info = new FileInfo(file);
            string destFileName = host + "/" + info.Name;            
            try
            {
                //yes, even though this is FTP, we can use the WebClient
                //it will see the ftp:// and use the appropriate protocol
                //internally
                WebClient client = new WebClient();
                client.Credentials = new NetworkCredential(username, password);
                byte[] response = client.UploadFile(destFileName, file);
                if (response.Length > 0)
                {
                    Console.WriteLine("Response: {0}", Encoding.ASCII.GetString(response));
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
