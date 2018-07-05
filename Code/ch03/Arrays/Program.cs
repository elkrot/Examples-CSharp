using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            //the following are all equivalent
            int[] array1 = new int[4];
            array1[0] = 13; array1[1] = 14; array1[2] = 15; array1[3] = 4;

            int[] array2 = new int[4] { 13, 14, 15, 16 };

            int[] array3 = new int[] { 13, 14, 15, 16 };

            int[] array4 = { 13, 14, 15, 16 };

            //rectangular array
            int[,] mArray1 = new int[,]
            {
                {1,2,3,4},
                {5,6,7,8},
                {9,10,11,12}
            };
            float val = mArray1[0, 1];//2

            //jagged array
            int[][] mArray2 = new int[3][];
            mArray2[0] = new int[] { 1, 2, 3 };
            mArray2[1] = new int[] { 4, 5, 6, 7, 8 };
            mArray2[2] = new int[] { 9, 10, 11, 12 };
            val = mArray2[0][1];//2
            

        }
    }
}
