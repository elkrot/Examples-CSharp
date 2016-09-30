// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// TraceTest.cs
// компиляция с  /reference:CondMethod.dll
// аргументы: А Б В
using System; 
using TraceFunctions; 

public class TraceClient 
{
   public static void Main(string[] args) 
   { 
      Trace.Message("Main Starting"); 
   
      if (args.Length == 0) 
      { 
          Console.WriteLine("No arguments have been passed"); 
      } 
      else 
      { 
          for( int i=0; i < args.Length; i++)    
          { 
              Console.WriteLine("Arg[{0}] is [{1}]",i,args[i]); 
          } 
      } 

       Trace.Message("Main Ending"); 
   } 
}

