using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Service;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class DictionaryAttractionTypeController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryAttractionTypeController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public ActionResult Attraction_Read([DataSourceRequest] DataSourceRequest request, string txtboxAttractionName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();
            tempDataSourceResult.Total = _dictionaryService.GetDictionaryAttractionTypeCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryAttractionTypeFilterPage(request.Page, request.PageSize, txtboxAttractionName);
            return Json(tempDataSourceResult);
        }
        public IActionResult Attraction()
        {
            return View();
        }
    }
}