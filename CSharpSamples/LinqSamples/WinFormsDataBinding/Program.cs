// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsDataBinding {
    static class Program 
    {
        // Следующий пример предполагает, что установлен проект примера DATA 
        // и файл Northwind.MDF находится в каталоге Data.
        private readonly static string dbPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\Data\NORTHWND.MDF"));
        private const string sqlServerInstance = @".\SQLEXPRESS";
        public readonly static string connString = "AttachDBFileName='" + dbPath + "';Server='" + sqlServerInstance + "';user instance=true;Integrated Security=SSPI;Connection Timeout=60";

        //Кроме того, файл Northwind.cs изменен по сравнению с тем, который создан SQLMetal,
        //чтобы включить в сущность Employee переопределенный метод ToString. Благодаря этому столбец ReportsToEmployee представления DataGridView
        //сможет отображать значения в приемлемом формате.
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.Run(new EmployeeForm());
        }
    }
}