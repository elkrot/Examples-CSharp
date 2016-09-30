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
    class PartialClassesMain
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("One argument required.");
                return;
            }

            // CharValues -- это класс partial: два его метода
            // определяются в файле CharTypesPublic.cs, а два других -
            // в файле CharTypesPrivate.cs. 
            int aCount = CharValues.CountAlphabeticChars(args[0]);
            int nCount = CharValues.CountNumericChars(args[0]);
            
            Console.Write("The input argument contains {0} alphabetic and {1} numeric characters", aCount, nCount);
        }
    }
}

