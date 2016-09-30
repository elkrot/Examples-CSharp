// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// FunctionClient.cs
// компиляция с  /reference:DigitCounter.dll;Factorial.dll
// аргументы: 3 5 10
using System; 
// Следующая директива using обеспечивает доступность типов,
// определенных в пространстве имен
using Functions;
class FunctionClient 
{ 
    public static void Main(string[] args) 
    { 
        Console.WriteLine("Function Client"); 

        if ( args.Length == 0 ) 
        {
            Console.WriteLine("Usage: FunctionTest ... "); 
            return; 
        } 

        for ( int i = 0; i < args.Length; i++ ) 
        { 
            int num = Int32.Parse(args[i]); 
            Console.WriteLine(
               "The Digit Count for String [{0}] is [{1}]", 
               args[i], 
               // Вызов статического метода NumberOfDigits
               // класса DigitCount:
               DigitCount.NumberOfDigits(args[i])); 
            Console.WriteLine(
               "The Factorial for [{0}] is [{1}]", 
                num,
               // Вызов статического метода Calc класса Factorial:
                Factorial.Calc(num) ); 
        } 
    } 
}

