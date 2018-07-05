using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Xsl;
using System.Diagnostics;

namespace XmlTransformDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            transform.Load("BookTransform.xslt");
            transform.Transform("LesMis.xml", "LesMis.html");
            
            Process.Start("LesMis.html");
        }
    }
}
