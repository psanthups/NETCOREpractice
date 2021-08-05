using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var model = new BookModel()                                        /*here we are passing tha language of the book by default to english (WeakReference are passing this byte controller)*/
            {                                                                  //and we have pass this var model in below view method(return view(MethodAccessException))
                //Language = "1"
            };
            //ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");         // this for creating adropdown using selectlist and its multiple parameters
            var group1 = new SelectListGroup() { Name = "Group1" };
            var group2 = new SelectListGroup() { Name = "Group2" };                   //this selectlistgroup is used to group the languages in dropdown
            var group3 = new SelectListGroup() { Name = "Group3" };
            ViewBag.Language = new List<SelectListItem>()                             //this is a best way to create dropdown using selectlistitem (this is the second method by using selectlistitem) in 1st method we need to get data of getlanguage(private) function
            {                                                                                          //in form of selectlistitem so we convert that and then convert this whole thing again to list by using ToList(). in 1st method we use getlanguage function text amd value properties as parameters here
                new SelectListItem(){Text = "English", Value = "1", Group = group1},
                new SelectListItem(){Text = "Hindi", Value = "2",Group = group1, Disabled = true},
                new SelectListItem(){Text = "Telugu", Value = "3", Group = group2},
                new SelectListItem(){Text = "French", Value = "4", Group = group2},
                new SelectListItem(){Text = "Tamil", Value = "5", Group = group3},
                new SelectListItem(){Text = "Urdu", Value = "6", Group = group3},
            };
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            //ViewBag.Language = new SelectList(new List<string>() { "English", "Hindi", "Telugu", "French" });    //here we are passing the languages from action method controller by selectList but desabled cause we passing languages by language model by creating a private method getlanguage method (note same line there in get mothod but i removed it
            ViewBag.Language = new SelectList(GetLanguage(), "Id", "Text");                                        //we can pass language using this selectlist in view file of select tag(inside) as we done previously by passing parameters to this selectlist.


            //ViewBag.IsSuccess = false;                                                                           //these two lines are used to not to show error if modelstate is not valid incase we not entered any data in addnew book form and submitted directly.
            //ViewBag.BookId = 0;                                                                                  // we can also fix this by writning "false" for viewbag.IsSuccess in addnewbook html view.so am commenting these lines
            //ModelState.AddModelError("", "this is custom error");                                                //this is custom error using validation summar. here we have to pass 2 parameters 1 is key(if we dont have kepp it blank like here) and other is error msg which we want to display.
            return View();
        }
        private List<LanguageModel> GetLanguage()
        {
            return new List<LanguageModel>()
            {
            new LanguageModel() { Id = 1, Text = "English" },
            new LanguageModel() { Id = 2, Text = "Hindi" },
            new LanguageModel() { Id = 3, Text = "Telugu" }
            };
        }
    } 
}
