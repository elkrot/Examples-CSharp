// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;

class NullableBoxing
{
    static void Main()
    {
        int? a;
        object oa;

        // Присвоение a значения Nullable<int> (value = default(int), hasValue = false).
        a = null;

        // Присвоение oa значения null (поскольку x==null), не упакованного "int?".
        oa = a;

        Console.WriteLine("Testing 'a' and 'boxed a' for null...");
        // Переменные, допускающие значение null, можно сравнивать с null.
        if (a == null)
        {
            Console.WriteLine("  a == null");
        }

        // Упакованные переменные, допускающие значение null, можно сравнивать с null,
        // поскольку упаковка допускающей значение null переменной, где HasValue==false,
        // задает ссылку на null.
        if (oa == null)
        {
            Console.WriteLine("  oa == null");
        }

        Console.WriteLine("Unboxing a nullable type...");
        int? b = 10;
        object ob = b;

        // Упакованные типы, допускающие значения null, можно распаковать
        int? unBoxedB = (int?)ob;
        Console.WriteLine("  b={0}, unBoxedB={0}", b, unBoxedB);

        // Распаковка допускающего значение null (nullable) типа, значение которого равно null, возможна, если
        // распаковка выполняется в nullable тип.
        int? unBoxedA = (int?)oa;
        if (oa == null && unBoxedA == null)
        {
            Console.WriteLine("  a and unBoxedA are null");
        }

        Console.WriteLine("Attempting to unbox into non-nullable type...");
        // Распаковка допускающего значение null (nullable) типа, значение которого равно null, создает
        // исключение, если распаковка выполняется в не-nullable тип.
        try
        {
            int unBoxedA2 = (int)oa;
        }
        catch (Exception e)
        {
            Console.WriteLine("  {0}", e.Message);
        }
    }

}
