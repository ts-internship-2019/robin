using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using iWasHere.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
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

        public ActionResult City_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionaryCity().ToDataSourceResult(request));
        }

        public IActionResult City()
        {
            List<Domain.Models.DictionaryCity> dictionaryCities = _dictionaryService.GetDictionaryCity();

            return View(dictionaryCities);
        }
    }
}