using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Service;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class MenuDictionaryController : Controller
    {
        private readonly DictionaryService _dictionaryService;
        public MenuDictionaryController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        public IActionResult Menu()
        {
            return View();
        }
    }
}