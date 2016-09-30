// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;

class NullableOperator
{
    static int? GetNullableInt()
    {
        return null;
    }

    static string GetStringValue()
    {
        return null;
    }

    static void Main()
    {
        // ?? : пример оператора.
        int? x = null;

        // y = x, если x не равно null; в этом случае y = -1.
        int y = x ?? -1;
        Console.WriteLine("y == " + y);                          

        // Присвоение i возвращаемого значения метода, если
        // возвращаемое значение не равно null; в этом случае присвоение
        // i значения int производится по умолчанию.
        int i = GetNullableInt() ?? default(int);
        Console.WriteLine("i == " + i);                          

        // ?? можно использовать также со ссылочными типами. 
        string s = GetStringValue();
        // Отображение содержимого s, если s не равно null; 
        // в этом случае отображается строка "Unspecified".
        Console.WriteLine("s = {0}", s ?? "null");
    }
}
