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
    // Этот файл содержит закрытые методы, определенные с помощью CharValues.
    partial class CharValues
    {
        private static bool IsAlphabetic(char ch)
        {
            if (ch >= 'a' && ch <= 'z')
                return true;
            if (ch >= 'A' && ch <= 'Z')
                return true;
            return false;
        }

        private static bool IsNumeric(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return true;
            return false;
        }
    }
}

