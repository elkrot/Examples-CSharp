using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;

namespace XPathDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            XPathDocument doc = new XPathDocument("LesMis.xml");
            XPathNavigator navigator = doc.CreateNavigator();
            XPathNodeIterator iter = navigator.Select("/Book/Chapters/Chapter");
            while (iter.MoveNext())
            {
                Console.WriteLine("Chapter: {0}", iter.Current.Value);
            }
            Console.WriteLine("Found {0} chapters", navigator.Evaluate("count(/Book/Chapters/Chapter)"));
            Console.ReadKey();
        }
    }
}
