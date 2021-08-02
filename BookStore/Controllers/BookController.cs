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
        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<ViewResult> GetAllBooks()
        {
           var data = await _bookRepository.GetAllBooks();
          
            return View(data);
        }
        //[Route("book-details/{id}", Name ="bookdetailsrouting")] //this used to pass custom url by tag helpers we use asp-route="bookdetailsrouting" in <a> tag of this actin view so we get book details in url of that page
        public async Task<ViewResult> GetBook(int id)
        {
           /* dynamic data = new System.Dynamic.ExpandoObject();
           data.book= _bookRepository.GetBookById(id);
           data.Name = "hurrey";*/  // this is for dynamic data use without using @model directive so that we can acces data in sntx of : @Model.book.Id(or)Author...

            
            var data =await _bookRepository.GetBookById(id);
            return View(data);
        }
        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }

        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            int id =await _bookRepository.AddNewBook(bookModel);
            if (id > 0)
            {
                return RedirectToAction(nameof(AddNewBook),new {isSuccess = true, bookId = id });
            }
            return View();
        }
    } 
}
