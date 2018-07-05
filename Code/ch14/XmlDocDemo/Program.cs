using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace XmlDocDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement bookElem = doc.CreateElement("Book");
            bookElem.SetAttribute("PublishYear", "2009");
            XmlElement titleElem = doc.CreateElement("Title");
            titleElem.InnerText = "Programming, art or engineering?";
            XmlElement authorElem = doc.CreateElement("Author");
            authorElem.InnerText = "Billy Bob";
            bookElem.AppendChild(titleElem);
            bookElem.AppendChild(authorElem);
            doc.AppendChild(bookElem);

            StringBuilder sb = new StringBuilder();
            //you could write to a file or any stream just as well
            using (StringWriter sw = new StringWriter(sb))
            using (XmlTextWriter xtw = new XmlTextWriter(sw))
            {
                xtw.Formatting = Formatting.Indented;
                doc.WriteContentTo(xtw);
                Console.WriteLine(sb.ToString());                
            }

            Console.ReadKey();            
        }
    }
}
