// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;

// Дополнительные сведения см. в файле ReadMe.html
namespace Samples
{
    public static class Program
    {
        const string HtmlFile = "System.Xml.Linq.html";
        
        public static void Main()
        {
            // Получить путь и имя сборки для отражения
            XDocument attr = new XDocument();
            Assembly assembly = Assembly.GetAssembly(attr.GetType());
            String AssemblyFile = assembly.CodeBase;

            // отразить в сборке
            Reflector reflector = new Reflector();
            reflector.Reflect(AssemblyFile);
            
            // создать HTML-документ
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(HtmlFile, settings);
            reflector.Transform(writer);
            writer.Close();
       
            // отобразить HTML-документ
            FileInfo fileInfo = new FileInfo(HtmlFile);
            if (fileInfo.Exists) Process.Start("iexplore.exe", fileInfo.FullName);
        }
    }
}