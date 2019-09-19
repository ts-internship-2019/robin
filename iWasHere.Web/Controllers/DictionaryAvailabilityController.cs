using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Service;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class DictionaryAvailabilityController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryAvailabilityController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public ActionResult DictionaryAvailability_Read([DataSourceRequest] DataSourceRequest request, string txtboxCityName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();

            tempDataSourceResult.Total = _dictionaryService.GetDictionaryAvailabilityCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryAvailabilityFilterPage(request.Page, request.PageSize, txtboxCityName);
            return Json(tempDataSourceResult);
        }

        public IActionResult Availability()
        {
            return View();
        }
    }
}