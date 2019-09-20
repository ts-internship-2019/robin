using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class AsyncController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}