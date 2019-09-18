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
    public class DictionaryCurrencyController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryCurrencyController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public ActionResult Currency_Read([DataSourceRequest] DataSourceRequest request, string txtboxCurrencyName)  
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();

            tempDataSourceResult.Total = _dictionaryService.GetDictionaryCurrencyCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryCurrencyFilterPage(request.Page, request.PageSize, txtboxCurrencyName);
            return Json(tempDataSourceResult);


            //return Json(_dictionaryService.GetDictionaryCurrency().ToDataSourceResult(request));
        }

        public IActionResult Currency()
        {
          //  List<Domain.Models.DictionaryCurrency> dictionaryCurrency = _dictionaryService.GetDictionaryCurrency();

            return View();  //dictionaryCurrency inside View
        }
    }
}