using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

class Program
{
    static void Main(string[] args)
    {

        HttpWebRequest request =
          (HttpWebRequest)WebRequest.Create
          ("http://mohamadpc/SimpleApp/default.aspx");

        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.UserAgent = "Sample client";

        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        byte[] postByteArray = encoding.GetBytes("val1=hello&val2=fellows!");
                
        Stream requestStream = request.GetRequestStream();
        requestStream.Write(postByteArray, 0, postByteArray.Length);
        requestStream.Close();


        WebResponse response =
          request.GetResponse();
        Stream responseStream = response.GetResponseStream();
        StreamReader myStreamReader = new StreamReader(responseStream);
        string responseData = myStreamReader.ReadToEnd();
        response.Close();


    }
}

