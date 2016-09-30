// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// tokens.cs
using System;
// Пространство имен System.Collections открыто для доступа:
using System.Collections;

// Объявление класса Tokens:
public class Tokens : IEnumerable
{
   private string[] elements;

   Tokens(string source, char[] delimiters)
   {
      // Синтаксический разбор строки на лексемы:
      elements = source.Split(delimiters);
   }

   // Реализация интерфейса IEnumerable:
   //   Объявление метода GetEnumerator(), 
   //   необходимого интерфейсу IEnumerable
   public IEnumerator GetEnumerator()
   {
      return new TokenEnumerator(this);
   }

   // Внутренний класс реализует интерфейс IEnumerator:
   private class TokenEnumerator : IEnumerator
   {
      private int position = -1;
      private Tokens t;

      public TokenEnumerator(Tokens t)
      {
         this.t = t;
      }

      // Объявление метода MoveNext, необходимого интерфейсу IEnumerator:
      public bool MoveNext()
      {
         if (position < t.elements.Length - 1)
         {
            position++;
            return true;
         }
         else
         {
            return false;
         }
      }

      // Объявление метода Reset, необходимого интерфейсу IEnumerator:
      public void Reset()
      {
         position = -1;
      }

      // Объявление свойства Current, необходимого интерфейсу IEnumerator:
      public object Current
      {
         get
         {
            return t.elements[position];
         }
      }
   }

   // Тестирование Tokens, TokenEnumerator

   static void Main()
   {
      // Тестирование Tokens путем разбиения строки на лексемы:
      Tokens f = new Tokens("This is a well-done program.", 
         new char[] {' ','-'});
      foreach (string item in f)
      {
         Console.WriteLine(item);
      }
   }
}

