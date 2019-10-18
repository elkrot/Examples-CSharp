// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// Hello1.cs
using System;
using System.Threading;

public class Hello1
{
   public static void Main()
   {
        int y =0;
        for (var i = 0; i < 100; i++)
        {
            for (var z = 0; z < 10; z++)
            {
                Thread.Sleep(1000);
                Console.SetCursorPosition(y++, z);
                Console.WriteLine((char)i);
            }
        }
        Console.ReadKey();

        System.Console.WriteLine("Hello, World!");
   }
}

