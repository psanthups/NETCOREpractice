using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            ViewBag.Title = "Site";
            dynamic data = new ExpandoObject();
            data.Id = 1;
            data.Name = "Bookish";
            ViewBag.Data = data;
            ViewBag.Type = new BookModel() { Id = 6, Author = "Author name" };

            ViewData["Title"] = "WEB";
            ViewData["Book"] = new BookModel() { Author = "hello", Id = 1 };

            return View();
        }
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
