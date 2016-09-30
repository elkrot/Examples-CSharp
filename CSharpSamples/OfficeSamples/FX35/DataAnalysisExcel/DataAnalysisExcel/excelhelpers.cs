// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using Microsoft.Office.Core;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace DataAnalysisExcel
{
    /// <summary>
    /// Сводное описание для ExcelHelpers.
    /// </summary>
    internal class ExcelHelpers
    {
        #region "функции для работы с листами"

        /// <summary>
        /// Избавляет от специальных символов в имени и обрезает его, чтобы имя
        /// можно было использовать в качестве имени листа в Excel. Имя обрезается до 31
        /// знака; знаки ":", "\", "/", "?", "*", "[" и "]" заменяются
        /// на "_".
        /// </summary>
        /// <param name="name">Первоначальное имя.</param>
        /// <returns>Имя без специальных знаков.</returns>
        static internal string CreateValidWorksheetName(string name)
        {
            // Длина имени листа не должна превышать 31 знака.
            System.Text.StringBuilder escapedString;

            if (name.Length <= 31)
            {
                escapedString = new System.Text.StringBuilder(name);
            }
            else
            {
                escapedString = new System.Text.StringBuilder(name, 0, 31, 31);
            }

            for (int i = 0; i < escapedString.Length; i++)
            {
                if (escapedString[i] == ':' ||
                    escapedString[i] == '\\' ||
                    escapedString[i] == '/' ||
                    escapedString[i] == '?' ||
                    escapedString[i] == '*' ||
                    escapedString[i] == '[' ||
                    escapedString[i] == ']')
                {
                    escapedString[i] = '_';
                }
            }

            return escapedString.ToString();
        }

        /// <summary>
        /// Возвращает лист с заданным именем.
        /// </summary>
        /// <param name="workbook">Книга, в которой находится лист.</param>
        /// <param name="name">Имя нужного листа.</param>
        /// <returns>Лист из книги с заданным именем.</returns>
        static internal Excel.Worksheet GetWorksheet(Excel.Workbook workbook, string name)
        {
            return workbook.Worksheets[name] as Excel.Worksheet;
        }

        /// <summary>
        /// Возвращает лист по заданному индексу.
        /// </summary>
        /// <param name="workbook">Книга, в которой находится лист.</param>
        /// <param name="index">Индекс нужного листа.</param>
        /// <returns>Лист из книги с заданным именем.</returns>
        static internal Excel.Worksheet GetWorksheet(Excel.Workbook workbook, int index)
        {
            return workbook.Worksheets[index] as Excel.Worksheet;
        }

        /// <summary>
        /// Возвращает активный лист из книги.
        /// </summary>
        /// <param name="workbook">Книга, в которой находится лист.</param>
        /// <returns>Активный лист из заданной книги.</returns>
        static internal Excel.Worksheet GetActiveSheet(Excel.Workbook workbook)
        {
            return workbook.ActiveSheet as Excel.Worksheet;
        }

        /// <summary>
        /// Возвращает имя листа или диаграммы.
        /// </summary>
        /// <param name="item">Лист или диаграмма.</param>
        /// <returns>Имя листа или диаграммы.</returns>
        static internal string GetName(object item)
        {
            string itemName;

            Excel.Worksheet sheet = item as Excel.Worksheet;
            if (sheet != null)
            {
                itemName = sheet.Name;
            }
            else
            {
                Excel.Chart chart = item as Excel.Chart;

                if (chart != null)
                {
                    itemName = chart.Name;
                }
                else
                {
                    itemName = null;
                }
            }

            return itemName;
        }

      #endregion
        #region "функции для работы с диапазонами"

        /// <summary>
        /// Возвращает объединение диапазонов.
        /// </summary>
        /// <param name="range1">Первый диапазон для объединения.</param>
        /// <param name="range2">Второй диапазон для объединения.</param>
        /// <param name="ranges">Массив диапазонов для объединения.</param>
        /// <returns>Возвращает диапазон, содержащий объединение всех переданных диапазонов.</returns>
        static internal Excel.Range Union(Excel.Range range1,
           Excel.Range range2,
           params Excel.Range[] ranges)
        {
            // Все диапазоны за исключением первых двух.
            object[] overflowParameters = new object[28];


            ranges.CopyTo(overflowParameters, 0);

            for (int i = ranges.Length;
               i < overflowParameters.Length;
               i++)
            {
                overflowParameters[i] = Type.Missing;
            }

            return range1.Application.Union(
               range1,
               range2,
               overflowParameters[0],
               overflowParameters[1],
               overflowParameters[2],
               overflowParameters[3],
               overflowParameters[4],
               overflowParameters[5],
               overflowParameters[6],
               overflowParameters[7],
               overflowParameters[8],
               overflowParameters[9],
               overflowParameters[10],
               overflowParameters[11],
               overflowParameters[12],
               overflowParameters[13],
               overflowParameters[14],
               overflowParameters[15],
               overflowParameters[16],
               overflowParameters[17],
               overflowParameters[18],
               overflowParameters[19],
               overflowParameters[20],
               overflowParameters[21],
               overflowParameters[22],
               overflowParameters[23],
               overflowParameters[24],
               overflowParameters[25],
               overflowParameters[26],
               overflowParameters[27]
               );
        }


        /// <summary>
        /// Возвращает пересечение диапазонов.
        /// </summary>
        /// <param name="range1">Первый диапазон для пересечения.</param>
        /// <param name="range2">Второй диапазон для пересечения.</param>
        /// <param name="ranges">Массив диапазонов для пересечения.</param>
        /// <returns>Возвращает диапазон, содержащий пересечение всех переданных диапазонов.</returns>
        static internal Excel.Range Intersect(Excel.Range range1,
           Excel.Range range2,
         params Excel.Range[] ranges)
        {
            // Все диапазоны за исключением первых двух.
            object[] overflowParameters = new object[28];


            ranges.CopyTo(overflowParameters, 0);

            for (int i = ranges.Length;
               i < overflowParameters.Length;
               i++)
            {
                overflowParameters[i] = Type.Missing;
            }

            return range1.Application.Intersect(
               range1,
               range2,
               overflowParameters[0],
               overflowParameters[1],
               overflowParameters[2],
               overflowParameters[3],
               overflowParameters[4],
               overflowParameters[5],
               overflowParameters[6],
               overflowParameters[7],
               overflowParameters[8],
               overflowParameters[9],
               overflowParameters[10],
               overflowParameters[11],
               overflowParameters[12],
               overflowParameters[13],
               overflowParameters[14],
               overflowParameters[15],
               overflowParameters[16],
               overflowParameters[17],
               overflowParameters[18],
               overflowParameters[19],
               overflowParameters[20],
               overflowParameters[21],
               overflowParameters[22],
               overflowParameters[23],
               overflowParameters[24],
               overflowParameters[25],
               overflowParameters[26],
               overflowParameters[27]
               );
        }

        /// <summary>
        /// Возвращает из книги диапазон с заданным именем.
        /// </summary>
        /// <param name="workbook">Книга, содержащая указанный именованный диапазон.</param>
        /// <param name="name">Имя нужного диапазона.</param>
        /// <returns>Диапазон с заданным именем из книги.</returns>
        internal static Excel.Range GetNamedRange(Excel.Workbook workbook, string name)
        {
            Excel.Name nameObject = workbook.Names.Item(
               name,
               Type.Missing,
               Type.Missing);

            return nameObject.RefersToRange;
        }

        /// <summary>
        /// Возвращает из заданного листа диапазон с заданным именем.
        /// </summary>
        /// <param name="worksheet">Лист, содержащий указанный именованный диапазон.</param>
        /// <param name="name">Имя нужного диапазона.</param>
        /// <returns>Диапазон с заданным именем из заданного листа.</returns>
        internal static Excel.Range GetNamedRange(Excel.Worksheet worksheet, string name)
        {
            return worksheet.get_Range(name, Type.Missing);
        }

        /// <summary>
        /// Возвращает диапазон со столбцом по указанному индексу диапазона.
        /// </summary>
        /// <param name="rowRange">Диапазон, содержащий указанный столбец.</param>
        /// <param name="column">Индекс нужного столбца из диапазона.</param>
        /// <returns>Диапазон, содержащий указанный столбец из заданного диапазона.</returns>
        internal static Excel.Range GetColumnFromRange(Excel.Range rowRange, int column)
        {
            return rowRange.Columns[column, Type.Missing] as Excel.Range;
        }

        /// <summary>
        /// Возвращает диапазон со строкой по указанному индексу диапазона.
        /// </summary>
        /// <param name="columnRange">Диапазон, содержащий указанную строку.</param>
        /// <param name="row">Индекс нужной строки из диапазона.</param>
        /// <returns>Диапазон, содержащий указанную строку из заданного диапазона.</returns>
        internal static Excel.Range GetRowFromRange(Excel.Range columnRange, int row)
        {
            return columnRange.Rows[row, Type.Missing] as Excel.Range;
        }

        /// <summary>
        /// Возвращает диапазон, состоящий из ячейки на пересечении указанных строки и столбца.
        /// </summary>
        /// <param name="range">Диапазон, содержащий нужную ячейку.</param>
        /// <param name="row">Индекс строки, содержащей нужную ячейку.</param>
        /// <param name="column">Индекс столбца,  содержащего нужную ячейку.</param>
        /// <returns></returns>
        internal static Excel.Range GetCellFromRange(Excel.Range range, int row, int column)
        {
            return range.Cells[row, column] as Excel.Range;
        }

        /// <summary>
        /// Возвращает значение заданного диапазона как объект.
        /// </summary>
        /// <param name="range">Диапазон, из которого следует извлечь значение.</param>
        /// <param name="address">Локальный адрес поддиапазона, из которого будет запрашиваться значение.</param>
        /// <returns>Возвращает значение ячейки из поддиапазона, указанного адресом.</returns>
        internal static Object GetValue(Excel.Range range, string address)
        {
            return range.get_Range(address, Type.Missing).Value2;
        }

        /// <summary>
        /// Возвращает значение заданного диапазона как значение типа double.
        /// </summary>
        /// <param name="range">Диапазон, из которого следует извлечь значение.</param>
        /// <returns>Возвращает значение из диапазона как значение типа double.</returns>
        internal static double GetValueAsDouble(Excel.Range range)
        {
            if (range.Value2 is double)
            {
                return (double)range.Value2;
            }

            return double.NaN;
        }

        /// <summary>
        /// Возвращает по указанным индексам значение ячейки как Double.
        /// </summary>
        /// <param name="sheet">Лист, содержащий нужную ячейку.</param>
        /// <param name="row">Строка листа, которая содержит ячейку.</param>
        /// <param name="column">Столбец листа, который содержит ячейку.</param>
        /// <returns>Возвращает значение ячейки по указанным индексам как double.</returns>
        internal static double GetValueAsDouble(Excel.Worksheet sheet, int row, int column)
        {
            Excel.Range subRange = ((Excel.Range)sheet.Cells[row, column]);

            return GetValueAsDouble(subRange);
        }

        /// <summary>
        /// Возвращает по указанным индексам значение ячейки как Double.
        /// </summary>
        /// <param name="range">Диапазон, содержащий нужную ячейку.</param>
        /// <param name="row">Строка диапазона, которая содержит ячейку.</param>
        /// <param name="column">Столбец диапазона, который содержит ячейку.</param>
        /// <returns>Возвращает значение ячейки по указанным индексам как double.</returns>
        internal static double GetValueAsDouble(Excel.Range range, int row, int column)
        {
            Excel.Range subRange = ((Excel.Range)range.Cells[row, column]);

            return GetValueAsDouble(subRange);
        }

        /// <summary>
        /// Возвращает значение из заданного диапазона как строку.
        /// </summary>
        /// <param name="range">Диапазон, из которого следует извлечь значение.</param>
        /// <returns>Возвращает значение из заданного диапазона как значение типа string.</returns>
        internal static string GetValueAsString(Excel.Range range)
        {
            if (!(range.Value2 == null))
            {
                return range.Value2.ToString();
            }

            return null;
        }

        /// <summary>
        /// Возвращает по указанным индексам значение ячейки как строку.
        /// </summary>
        /// <param name="range">Диапазон, содержащий нужную ячейку.</param>
        /// <param name="row">Строка диапазона, которая содержит ячейку.</param>
        /// <param name="column">Столбец диапазона, который содержит ячейку.</param>
        /// <returns>Возвращает значение ячейки по указанным индексам как значение string.</returns>
        internal static string GetValueAsString(Excel.Range range, int row, int column)
        {
            Excel.Range subRange = ((Excel.Range)range.Cells[row, column]);

            return GetValueAsString(subRange);
        }

        /// <summary>
        /// Возвращает по указанным индексам значение ячейки как строку.
        /// </summary>
        /// <param name="sheet">Лист, содержащий нужную ячейку.</param>
        /// <param name="row">Строка листа, которая содержит ячейку.</param>
        /// <param name="column">Столбец листа, который содержит ячейку.</param>
        /// <returns>Возвращает значение ячейки по указанным индексам как значение string.</returns>
        internal static string GetValueAsString(Excel.Worksheet sheet, int row, int column)
        {
            Excel.Range subRange = ((Excel.Range)sheet.Cells[row, column]);

            return GetValueAsString(subRange);
        }

      #endregion
        #region "функции для работы с фигурами"
        /// <summary>
        /// Возвращает фигуру с данным именем из активного листа.
        /// </summary>
        /// <param name="workbook">Книга, содержащая фигуру.</param>
        /// <param name="name">Имя фигуры.</param>
        /// <returns>Возвращает фигуру с данным именем из активной книги.</returns>
        static internal Excel.Shape GetShape(Excel.Workbook workbook, string name)
        {
            return GetShape(GetActiveSheet(workbook), name);
        }

        /// <summary>
        /// Возвращает фигуру по данному индексу из активного листа.
        /// </summary>
        /// <param name="workbook">Книга, содержащая фигуру.</param>
        /// <param name="index">Индекс фигуры.</param>
        /// <returns>Возвращает фигуру по данному индексу из активного листа.</returns>
        static internal Excel.Shape GetShape(Excel.Workbook workbook, int index)
        {
            return GetShape(GetActiveSheet(workbook), index);
        }

        /// <summary>
        /// Возвращает фигуру с данным именем из данного листа.
        /// </summary>
        /// <param name="worksheet">Лист, содержащий фигуру.</param>
        /// <param name="name">Имя фигуры.</param>
        /// <returns>Возвращает фигуру с данным именем из данного листа.</returns>
        static internal Excel.Shape GetShape(Excel.Worksheet worksheet, string name)
        {
            return worksheet.Shapes._Default(name);
        }

        /// <summary>
        /// Возвращает фигуру по данному индексу из данного листа.
        /// </summary>
        /// <param name="worksheet">Лист, содержащий фигуру.</param>
        /// <param name="index">Индекс фигуры.</param>
        /// <returns>Возвращает фигуру по данному индексу из данного листа.</returns>
        static internal Excel.Shape GetShape(Excel.Worksheet worksheet, int index)
        {
            return worksheet.Shapes._Default(index);
        }
      #endregion
        #region "функции для работы с датами"
        // Даты в Excel основаны на значении ''1 января 1900''.
        // Имеются две причины для использования 30 декабря 1899 г.
        // Одна причина - это то, что дата 29/2/1900 допустима в Excel (в
        // действительности, это - недопустимая дата: 1900 - не високосный год);
        // другая - это то, что 0 в Excel соответствует 0 января.
        private readonly static DateTime timeOrigin =
           new DateTime(1899, 12, 30, 0, 0, 0, 0);

        /// <summary>
        /// Возвращает десятичный эквивалент даты для Excel.
        /// </summary>
        /// <param name="dateValue">Преобразуемая дата.</param>
        /// <returns>Десятичное представление даты для Excel.</returns>
        internal static double GetSerialDate(DateTime dateValue)
        {
            TimeSpan since1900 = dateValue - timeOrigin;

            return since1900.Days;
        }

        /// <summary>
        /// Возвращает значение DateTime десятичного представления даты в Excel.
        /// </summary>
        /// <param name="serial">Десятичное значение даты из Excel.</param>
        /// <returns>Значение DateTime, эквивалентное десятичному представлению даты в Excel.</returns>
        internal static DateTime GetDateTime(double serial)
        {
            TimeSpan since1900 = new TimeSpan((int)serial, 0, 0, 0);

            return timeOrigin.Add(since1900);
        }

      #endregion


        public static stdole.IPictureDisp Convert(System.Drawing.Image image)
        {
            return ImageToPictureConverter.Convert(image);
        }

        /// <summary>
        /// Класс для предоставления защищенного метода GetIPictureDispFromPicture
        /// из AxHost.
        /// </summary>
        sealed private class ImageToPictureConverter : System.Windows.Forms.AxHost
        {
            private ImageToPictureConverter()
                : base(null)
            {
            }

            public static stdole.IPictureDisp Convert(System.Drawing.Image image)
            {
                return (stdole.IPictureDisp)System.Windows.Forms.AxHost.GetIPictureDispFromPicture(image);
            }
        }
    }
}
