﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Marin.Models;
using Microsoft.AspNetCore.Authorization;

namespace Marin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Här kan du se information om appen";

            return View();
        }

        public IActionResult CreateBudget()
        {
          return View();
        }
    }
}
