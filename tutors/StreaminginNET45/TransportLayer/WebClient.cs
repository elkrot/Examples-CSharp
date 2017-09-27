using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();

            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = 
                "application/x-www-form-urlencoded";
           
            //post data
            Stream requestStream = client.OpenWrite
                ("http://mohamadpc/SimpleApp/default.aspx", "POST");

            byte[] postByteArray = encoding.GetBytes("val1=hello&val2=fellows!");
            requestStream.Write(postByteArray, 0, postByteArray.Length);
            
            //The data is sent to the server when the stream is closed
            requestStream.Close(); 

            //get data
            requestStream = client.OpenRead
                ("http://mohamadpc/SimpleApp/default.aspx");

            StreamReader sr = new StreamReader(requestStream);
            string data = sr.ReadToEnd();
            requestStream.Close();
        }
    }

