using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Service;
using iWasHere.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class DictionaryCountryController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryCountryController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public ActionResult Country_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionaryCountry().ToDataSourceResult(request));
        }

        public IActionResult Country()
        {
            List<Domain.Models.DictionaryCountry> dictionaryCountries = _dictionaryService.GetDictionaryCountry();

            return View(dictionaryCountries);
           
        }
    }
}