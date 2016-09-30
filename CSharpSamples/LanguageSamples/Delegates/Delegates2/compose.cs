// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// compose.cs
using System;

delegate void MyDelegate(string s);

class MyClass
{
    public static void Hello(string s)
    {
        Console.WriteLine("  Hello, {0}!", s);
    }

    public static void Goodbye(string s)
    {
        Console.WriteLine("  Goodbye, {0}!", s);
    }

    public static void Main()
    {
        MyDelegate a, b, c, d;

        // Создание объекта делегата a, который ссылается 
        // на метод Hello:
        a = new MyDelegate(Hello);
        // Создание объекта делегата b, который ссылается 
        // на метод Goodbye:
        b = new MyDelegate(Goodbye);
        // Два делегата a и b объединяются, чтобы создать делегат c, 
        // который вызывает оба метода в заявке:
        c = a + b;
        // Удаление a из составного делегата при сохранении d, 
        // который вызывает только метод Goodbye:
        d = c - a;

        Console.WriteLine("Invoking delegate a:");
        a("A");
        Console.WriteLine("Invoking delegate b:");
        b("B");
        Console.WriteLine("Invoking delegate c:");
        c("C");
        Console.WriteLine("Invoking delegate d:");
        d("D");
    }
}

