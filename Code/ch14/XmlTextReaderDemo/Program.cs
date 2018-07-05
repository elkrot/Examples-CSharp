using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace XmlTextReaderDemo
{
    class Program
    {
        const string sourceXml =
            "<Book PublishYear=\"2009\">" +
            "<Title>Programming, art or engineering?</Title>" +
            "<Author>Billy Bob</Author>" +
            "</Book>";

        static void Main(string[] args)
        {
            string publishYear = null, author = null;
            using (StringReader reader = new StringReader(sourceXml)) 
            using (XmlTextReader xmlReader = new XmlTextReader(reader))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlReader.Name == "Book")
                        {
                            if (xmlReader.MoveToAttribute("PublishYear"))
                            {
                                publishYear = xmlReader.Value;
                            }
                        }
                        else if (xmlReader.Name == "Author")
                        {
                            xmlReader.Read();
                            author = xmlReader.Value;
                        }
                    }
                }
            }

            Console.WriteLine("Publish Year: {0}", publishYear);
            Console.WriteLine("Author: {0}", author);

            Console.ReadKey();



        }
    }
}
