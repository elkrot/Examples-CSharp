// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SampleSupport;
using SampleQueries;
using System.IO;
using DataSetSampleQueries;

// Дополнительные сведения см. в файле ReadMe.html
namespace SampleQueries
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            List<SampleHarness> harnesses = new List<SampleHarness>();

            // Примеры Linq:
            LinqSamples linqHarness = new LinqSamples();
            harnesses.Add(linqHarness);

            // Примеры Linq To SQL:
            LinqToSqlSamples linqToSqlHarness = new LinqToSqlSamples();
            harnesses.Add(linqToSqlHarness);

            // Примеры LinqToXml:
            LinqToXmlSamples linqToXmlHarness = new LinqToXmlSamples();
            harnesses.Add(linqToXmlHarness);

            // Примеры DataSetLinq:
            DataSetLinqSamples dsLinqSamples = new DataSetLinqSamples();
            harnesses.Add(dsLinqSamples);
            
            // XQueryUseCases:
            XQueryUseCases xqueryHarness = new XQueryUseCases();
            harnesses.Add(xqueryHarness);

            if (args.Length >= 1 && args[0] == "/runall") {
                foreach (SampleHarness harness in harnesses)
                {
                    harness.RunAllSamples();
                }
            }
            else {
                Application.EnableVisualStyles();
                
                using (SampleForm form = new SampleForm("LINQ Project Sample Query Explorer", harnesses))
                {
                    form.ShowDialog();
                }
            }
        }
    }
}