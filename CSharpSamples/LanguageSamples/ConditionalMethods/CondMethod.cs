// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// CondMethod.cs
// компиляция с  /target:library /d:DEBUG
using System; 
using System.Diagnostics;
namespace TraceFunctions 
{
   public class Trace 
   { 
       [Conditional("DEBUG")] 
       public static void Message(string traceMessage) 
       { 
           Console.WriteLine("[TRACE] - " + traceMessage); 
       } 
   } 
}

