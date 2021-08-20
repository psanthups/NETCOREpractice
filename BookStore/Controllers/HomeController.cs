using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using BookStore.Models;

namespace BookStore.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
       /* [ViewData]
        public string Title { get; set; }
        [ViewData]
        public BookModel book { get; set; }*/

        [Route("")]
        public ViewResult Index()
        {
           
           /*Title = "Home";
           book = new BookModel() { Author = "hello", Id = 1 };*/

            return View();
        }

        [Route("~/About-us")]
        public ViewResult Aboutus()
        {
            return View();
        }
        public ViewResult Contactus()
        {
            return View();
        }
    }
}
