// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// tokens2.cs
using System;
using System.Collections;

public class Tokens: IEnumerable
{
   private string[] elements;

   Tokens(string source, char[] delimiters)
   {
      elements = source.Split(delimiters);
   }

   // Реализация интерфейса IEnumerable:

   public TokenEnumerator GetEnumerator() // версия не-IEnumerable
   {
      return new TokenEnumerator(this);
   }

   IEnumerator IEnumerable.GetEnumerator() // версия IEnumerable
   {
      return (IEnumerator) new TokenEnumerator(this);
   }

   // Внутренний класс реализует интерфейс IEnumerator:

   public class TokenEnumerator: IEnumerator
   {
      private int position = -1;
      private Tokens t;

      public TokenEnumerator(Tokens t)
      {
         this.t = t;
      }

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

      public void Reset()
      {
         position = -1;
      }

      public string Current // версия, не являющаяся IEnumerator: типобезопасная
      {
         get
         {
            return t.elements[position];
         }
      }

      object IEnumerator.Current // версия IEnumerator: возвращает объект
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
      Tokens f = new Tokens("This is a well-done program.", 
         new char [] {' ','-'});
      foreach (string item in f) // попытка преобразования строки в целое число
      {
         Console.WriteLine(item);
      }
   }
}

