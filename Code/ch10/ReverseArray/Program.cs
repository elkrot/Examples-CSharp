using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReverseArray
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            PrintArray<int>(array);
            Reverse<int>(array);
            PrintArray<int>(array);
            
            Console.ReadKey();
        }

        private static void PrintArray<T>(T[] array)
        {
            foreach (T val in array)
            {
                Console.Write(val + " ");
            }
            Console.WriteLine();
        }

        private static void Reverse<T>(T[] array)
        {
            int left = 0, right = array.Length - 1;
            while (left < right)
            {
                T temp = array[left];
                array[left] = array[right];
                array[right] = temp;
                left++;
                right--;
            }
        }
    }
}
