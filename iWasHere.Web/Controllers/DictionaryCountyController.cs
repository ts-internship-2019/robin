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
    public class DictionaryCountyController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryCountyController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public ActionResult County_Read([DataSourceRequest] DataSourceRequest request, string txtboxCountyName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();
            tempDataSourceResult.Total = _dictionaryService.GetDictionaryCountyCount();
            //tempDataSourceResult.Data = _dictionaryService.GetDictionaryCountyPage(request.Page,request.PageSize);

            return Json(tempDataSourceResult);

            //return Json(_dictionaryService.GetDictionaryCounty().ToDataSourceResult(request));
        }

        public IActionResult County()
        {
            List<Domain.Models.DictionaryCounty> dictionaryCounty = _dictionaryService.GetDictionaryCounty();

            return View(dictionaryCounty);
            //return View();
        }
    }
}