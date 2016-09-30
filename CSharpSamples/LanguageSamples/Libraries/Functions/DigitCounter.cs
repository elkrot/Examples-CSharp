// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// DigitCounter.cs
// компилировать с /target:library
using System; 

// Объявление того же пространства имен, что и в Factorial.cs, что 
// позволяет добавлять типы в то же пространство имен.
namespace Functions 
{
    public class DigitCount 
    {
        // Статический метод NumberOfDigits рассчитывает число
        // цифровых символов в переданной строке:
        public static int NumberOfDigits(string theString) 
        {
            int count = 0; 
            for ( int i = 0; i < theString.Length; i++ ) 
            {
                if ( Char.IsDigit(theString[i]) ) 
                {
                    count++; 
                }
            }
            return count;
        }
    }
}

