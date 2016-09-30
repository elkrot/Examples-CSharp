// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// CSharpServer.cs
// компилировать с /target:library
// команда, выполняемая после построения: regasm CSharpServer.dll /tlb:CSharpServer.tlb

using System;
using System.Runtime.InteropServices;
namespace CSharpServer
{
   // Поскольку интерфейс .NET Framework и компонентный класс должны вести себя как 
   // COM-объекты, им необходимо присвоить GUID.
   [Guid("DBE0E8C4-1C61-41f3-B6A4-4E2F353D3D05")]
   public interface IManagedInterface
   {
      int PrintHi(string name);
   }

   [Guid("C6659361-1625-4746-931C-36014B146679")]
   public class InterfaceImplementation : IManagedInterface
   {
      public int PrintHi(string name)
      {
         Console.WriteLine("Hello, {0}!", name);
         return 33;
      }
   }
}

