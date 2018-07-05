using System;
using Excel = Microsoft.Office.Interop.Excel;

namespace DynamicTypesInExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new Excel.Application();
            app.Visible = true;
            app.Workbooks.Add();
            //to avoid a lot of casting, use dynamic
            dynamic sheet = app.ActiveSheet;
            sheet.Cells[1, "A"] = 13;
            sheet.Cells[2, "A"] = 13;
            sheet.Cells[3, "A"] = "=A1*A2";
            sheet.Columns[1].AutoFit();
        }
    }
}
