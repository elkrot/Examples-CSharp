// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;


using nwind;
[assembly: CLSCompliant(true)]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: ComVisible(false)]

namespace LinqToNorthwind {
    class Program
    {
        [STAThread()]
        static void Main()
        {
            // Предполагается, что:
            // 1. На вашем компьютере установлен экспресс-выпуск SQL Server 2005
            // 2. Устанавливается каталог Data Sample, содержащий БД "Борей".
            // Если БД "Борей" уже установлена, необходимо изменить строку подключения на
            // Northwind db = new Northwind("Server=.\\SQLExpress;Database=Northwind;Trusted_Connection=True");
            // Следует изменить путь к MDF-файлу
            // Northwind db = new Northwind("c:\\northwind\\northwnd.mdf");

            string dbPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\Data\NORTHWND.MDF"));
            string sqlServerInstance = @".\SQLEXPRESS";
            string connString = "AttachDBFileName='" + dbPath + "';Server='" + sqlServerInstance + "';user instance=true;Integrated Security=SSPI; Pooling=false; Connection Timeout=60";

            Northwind db = new Northwind(connString);
            db.Log = Console.Out;
            Samples.Sample1(db);
            Console.ReadLine();
        }
    }
}
