using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace XmlTextWriterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (XmlTextWriter xtw = new XmlTextWriter(sw))
            {
                xtw.Formatting = Formatting.Indented;

                xtw.WriteStartElement("Book");

                xtw.WriteAttributeString("PublishYear", "2009");
                xtw.WriteStartElement("Title");
                xtw.WriteString("Programming, art or engineering?");
                xtw.WriteEndElement();

                xtw.WriteStartElement("Author");
                xtw.WriteString("Billy Bob");
                xtw.WriteEndElement();

                xtw.WriteEndElement();
            }

            Console.WriteLine(sb.ToString());

            Console.ReadKey();
        }
    }
}
