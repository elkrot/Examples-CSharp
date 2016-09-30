// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
using System;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace PythonSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading helloworld.py...");

            ScriptRuntime py = Python.CreateRuntime();
            dynamic helloworld = py.UseFile("helloworld.py");

            Console.WriteLine("helloworld.py loaded!");

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(helloworld.welcome("Employee #{0}"), i);
            }
            Console.ReadLine();
        }
    }
}
