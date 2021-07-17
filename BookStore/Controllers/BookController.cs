using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        public BookController()
        {
            _bookRepository = new BookRepository();
        }
        public ViewResult GetAllBooks()
        {
           var data = _bookRepository.GetAllBooks();
          
            return View(data);
        }
        //[Route("book-details/{id}", Name ="bookdetailsrouting")] //this used to pass custom url by tag helpers we use asp-route="bookdetailsrouting" in <a> tag of this actin view so we get book details in url of that page
        public ViewResult GetBook(int id)
        {
           /* dynamic data = new System.Dynamic.ExpandoObject();
           data.book= _bookRepository.GetBookById(id);
           data.Name = "hurrey";*/  // this is for dynamic data use without using @model directive so that we can acces data in sntx of : @Model.book.Id(or)Author...

            
            var data = _bookRepository.GetBookById(id);
            return View(data);
        }
        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }

        public ViewResult AddNewBook()
        {
            return View();
        }

        [HttpPost]
        public ViewResult AddNewBook(BookModel bookModel)
        {
            return View();
        }
    }
}
