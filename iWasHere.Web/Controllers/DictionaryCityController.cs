using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Model;
using iWasHere.Domain.Service;
using iWasHere.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class DictionaryCityController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryCityController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }
        public IActionResult City()
        {
            List<DictionaryCity> dictionaryLandmarkTypeModels = _dictionaryService.GetDictionaryCity();

            return View(dictionaryLandmarkTypeModels);
            //return View();
        }
    }
}