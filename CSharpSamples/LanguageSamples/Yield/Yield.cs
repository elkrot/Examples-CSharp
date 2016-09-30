// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections.Generic;
using System.Text;

namespace Yield
{
    class Yield
    {
        public static class NumberList
        {
            // Создание массива целых чисел.
            public static int[] ints = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377 };

            // Определение свойства, которое возвращает только четные числа.
            public static IEnumerable<int> GetEven()
            {
                // Использование метода yield для возврата из списка четных чисел.
                foreach (int i in ints)
                    if (i % 2 == 0)
                        yield return i;
            }

            // Определение свойства, которое возвращает только четные числа.
            public static IEnumerable<int> GetOdd()
            {
                // Использование метода yield для возврата из списка только нечетных чисел.
                foreach (int i in ints)
                    if (i % 2 == 1)
                        yield return i;
            }
        }

        static void Main(string[] args)
        {

            // Отображение четных чисел.
            Console.WriteLine("Even numbers");
            foreach (int i in NumberList.GetEven())
                Console.WriteLine(i);

            // Отображение нечетных чисел.
            Console.WriteLine("Odd numbers");
            foreach (int i in NumberList.GetOdd())
                Console.WriteLine(i);
        }
    }
}

