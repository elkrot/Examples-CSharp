using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace EntityFrameworkDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //get a list of all entities
            BookEntities entities = new BookEntities();
            PrintAll(entities.Books);

            //create a new entity
            Book newBook = new Book();
            newBook.Title = "C# 4 How-to";
            newBook.PublishYear = 2010;
            entities.AddToBooks(newBook);
            entities.SaveChanges();
            PrintAll(entities.Books);
            entities.DeleteObject(newBook);

            //query for entity with ID of 1
            ObjectQuery<Book> bookQuery = new ObjectQuery<Book>("SELECT VALUE Book FROM BookEntities.Books AS Book", 
                                                                entities).Where("it.ID = 1");
            var books = from book in entities.Books where book.ID == 1 select book;
            PrintAll(books);
            foreach (var Book in bookQuery)
            {
                
            }


            Console.ReadKey();
        }

        private static void PrintAll(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine("{0}, {1}   {2}", book.ID, book.Title, book.PublishYear);
            }
            Console.WriteLine();
        }
    }
}
