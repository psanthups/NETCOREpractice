﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Areas.Financial.Controllers
{
    [Area("Financial")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Graph()
        {
            return View();
        }
    }
}
