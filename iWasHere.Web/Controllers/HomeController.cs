using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Web.Models;

namespace iWasHere.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Ticket()
        {
            return View();
        }


        public IActionResult Country()
        {
            return View();
        }

        public IActionResult County()
        {
            return View();
        }

        public IActionResult LandmarkDetails()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Currency()
        {
            return View();
        }
    }
}
