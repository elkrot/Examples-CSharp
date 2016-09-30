// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// bookstore.cs
using System;

// Набор классов для обслуживания книжного магазина:
namespace Bookstore 
{
   using System.Collections;

   // Описание книги в списке книг:
   public struct Book
   {
      public string Title;        // Название книги.
      public string Author;       // Автор книги.
      public decimal Price;       // Цена книги.
      public bool Paperback;      // Книга в бумажной обложке?

      public Book(string title, string author, decimal price, bool paperBack)
      {
         Title = title;
         Author = author;
         Price = price;
         Paperback = paperBack;
      }
   }

   // Объявление типа делегата для обработки книги:
   public delegate void ProcessBookDelegate(Book book);

   // Обслуживание базы данных книг.
   public class BookDB
   {
      // Список всех книг в базе данных:
      ArrayList list = new ArrayList();   

      // Добавление книги в базу данных:
      public void AddBook(string title, string author, decimal price, bool paperBack)
      {
         list.Add(new Book(title, author, price, paperBack));
      }

      // Вызов переданного делегата каждой книге в бумажной обложке для ее обработки: 
      public void ProcessPaperbackBooks(ProcessBookDelegate processBook)
      {
         foreach (Book b in list) 
         {
            if (b.Paperback)
            // Вызов делегата:
               processBook(b);
         }
      }
   }
}

// Использование классов книжного магазина:
namespace BookTestClient
{
   using Bookstore;

   // Класс для общих и средних цен на книги:
   class PriceTotaller
   {
      int countBooks = 0;
      decimal priceBooks = 0.0m;

      internal void AddBookToTotal(Book book)
      {
         countBooks += 1;
         priceBooks += book.Price;
      }

      internal decimal AveragePrice()
      {
         return priceBooks / countBooks;
      }
   }

   // Класс для тестирования базы данных книг:
   class Test
   {
      // Печать названия книги.
      static void PrintTitle(Book b)
      {
         Console.WriteLine("   {0}", b.Title);
      }

      // Выполнение начинается здесь.
      static void Main()
      {
         BookDB bookDB = new BookDB();

         // Инициализация базы данных с использованием нескольких книг:
         AddBooks(bookDB);      

         // Печать названий всех книг в бумажной обложке:
         Console.WriteLine("Paperback Book Titles:");
         // Создание нового объекта делегата, связанного со статическим 
         // методом Test.PrintTitle:
         bookDB.ProcessPaperbackBooks(new ProcessBookDelegate(PrintTitle));

         // Получение средней цены книги в бумажной обложке при помощи
         // объекта PriceTotaller:
         PriceTotaller totaller = new PriceTotaller();
         // Создание нового объекта-делегата, связанного с нестатическим объектом 
         // методом AddBookToTotal объекта суммарных показателей:
         bookDB.ProcessPaperbackBooks(new ProcessBookDelegate(totaller.AddBookToTotal));
         Console.WriteLine("Average Paperback Book Price: ${0:#.##}",
            totaller.AveragePrice());
      }

      // Инициализация базы данных книг с использованием  нескольких тестовых книг:
      static void AddBooks(BookDB bookDB)
      {
         bookDB.AddBook("The C Programming Language", 
            "Brian W. Kernighan and Dennis M. Ritchie", 19.95m, true);
         bookDB.AddBook("The Unicode Standard 2.0", 
            "The Unicode Consortium", 39.95m, true);
         bookDB.AddBook("The MS-DOS Encyclopedia", 
            "Ray Duncan", 129.95m, false);
         bookDB.AddBook("Dogbert's Clues for the Clueless", 
            "Scott Adams", 12.00m, true);
      }
   }
}

