// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections.Generic;
using System.Text;

namespace PartialClassesExample
{
    // Благодаря ключевому слову "partial" можно дополнительные методы, поля и
    // свойства данного класса определять в других .cs-файлах.
    // Этот файл содержит открытые методы, определенные с помощью CharValues.
    partial class CharValues
    {
        public static int CountAlphabeticChars(string str)
        {
            int count = 0;
            foreach (char ch in str)
            {
                // Метод IsAlphabetic определяется в файле CharTypesPrivate.cs
                if (IsAlphabetic(ch))
                    count++;
            }
            return count;
        }
        public static int CountNumericChars(string str)
        {
            int count = 0;
            foreach (char ch in str)
            {
                // Метод IsNumeric определяется в файле CharTypesPrivate.cs
                if (IsNumeric(ch))
                    count++;
            }
            return count;
        }

    }
}

