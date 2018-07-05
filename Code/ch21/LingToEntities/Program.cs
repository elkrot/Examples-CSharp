using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace LingToEntities
{
    class Program
    {
        static void Main(string[] args)
        {
            //query all books
            BooksEntities bookContext = new BooksEntities();
            var books = from book in bookContext.BookSet select book;
            foreach (var book in books)
            {
                //load the author info
                book.AuthorReference.Load();
                Console.WriteLine("{0}, {1} {2}", book.Title, book.Author.FirstName, book.Author.LastName);
            }
            Console.WriteLine();

            Console.WriteLine("Textbooks:");
            var textBooks = from book in bookContext.BookSet where book is Textbook select book as Textbook;
            foreach (var textBook in textBooks)
            {
                //load the author info
                textBook.AuthorReference.Load();
                Console.WriteLine("{0}, {1} {2} - {3}", textBook.Title, textBook.Author.FirstName, textBook.Author.LastName, textBook.Subject);
            }
            Console.WriteLine();
            //add a book      
            Author me = new Author();
            me.FirstName = "Ben";
            me.LastName = "Watson";
            Book myBook= new Book();
            myBook.Author = me;
            myBook.Title = "C# 4.0 How-To";
            
            bookContext.AddToBookSet(myBook);
            
            bookContext.SaveChanges();
            Console.WriteLine("Added my book:");
            foreach (var book in from b in bookContext.BookSet select b)
            {
                book.AuthorReference.Load();
                Console.WriteLine("{0}, {1} {2}", book.Title, book.Author.FirstName, book.Author.LastName);
            }
            
            //delete the book
            
            bookContext.DeleteObject(myBook);
            bookContext.DeleteObject(me);
            bookContext.SaveChanges();
            Console.ReadKey();
        }
    }
}
