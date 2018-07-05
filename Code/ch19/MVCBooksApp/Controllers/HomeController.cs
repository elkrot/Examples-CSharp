using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBooksApp.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        MVCBooksApp.Models.BooksDBEntities _db = new Models.BooksDBEntities();

        public ActionResult Index()
        {
            //return a viewn named Index
            return View(_db.Books.ToList());
        }

        //return the default Create view
        public ActionResult Create()
        {
            return View();
        }

        //called when the user submits the new book back to the server
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(
            [Bind(Exclude="Id")]
            MVCBooksApp.Models.Book newBook)
        {
            if (!ModelState.IsValid)
                return View();
            _db.AddToBooks(newBook);
            _db.SaveChanges();
            //send us back to the Index
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            //LINQ expression - see Chapter 21
            Models.Book book = (from b in _db.Books 
                                where b.ID == id 
                                select b).First();
            return View(book);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Models.Book editBook)
        {
            //get the original book from the database
            Models.Book book = (from b in _db.Books
                                where b.ID == editBook.ID
                                select b).First();
            if (!ModelState.IsValid)
                return View(book);
            //apply changes back to the original
            _db.ApplyCurrentValues(book.EntityKey.EntitySetName, editBook);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
