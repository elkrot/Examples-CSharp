using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XmlDocReader
{
    class Program
    {
        const string sourceXml = 
            "<Book PublishYear=\"2009\">"+
            "<Title>Programming, art or engineering?</Title>"+
            "<Author>Billy Bob</Author>"+
            "</Book>";

        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(sourceXml);

            Console.WriteLine("Publish Year: {0}", doc.GetElementsByTagName("Book")[0].Attributes["PublishYear"].Value);
            Console.WriteLine("Author: {0}", doc.GetElementsByTagName("Author")[0].InnerText);

            Console.ReadKey();
        }
    }
}
