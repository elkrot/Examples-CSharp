using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XMLWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (FileStream fs = new FileStream
                (@"C:\files\XMLFile1.xml",
                FileMode.Create))
            {
                using (XmlWriter xw = XmlWriter.Create(fs, settings))
                {
                    xw.WriteStartElement("Books");

                    xw.WriteStartElement("Book");
                    xw.WriteAttributeString("id", "1");
                    xw.WriteElementString("Title", "Streaming in .NET");
                    xw.WriteElementString("Category", "IT");
                }
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
