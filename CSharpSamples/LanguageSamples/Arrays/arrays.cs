// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// arrays.cs
using System;
class DeclareArraysSample
{
    public static void Main()
    {
        // Одномерный массив
        int[] numbers = new int[5];

        // Многомерный массив
        string[,] names = new string[5,4];

        // Массив массивов (нерегулярный массив)
        byte[][] scores = new byte[5][];

        // Создание массивf массивов
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = new byte[i+3];
        }

        // Печать длины каждой строки
        for (int i = 0; i < scores.Length; i++)
        {
            Console.WriteLine("Length of row {0} is {1}", i, scores[i].Length);
        }
    }
}


