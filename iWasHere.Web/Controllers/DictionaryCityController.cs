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

        public ActionResult City_Read([DataSourceRequest] DataSourceRequest request,string txtboxCityName, int cmbboxCountyName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();

            tempDataSourceResult.Total =_dictionaryService.GetDictionaryCityCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryCityFilterPage(request.Page,request.PageSize, txtboxCityName, cmbboxCountyName);
            return Json(tempDataSourceResult);
        }

        public ActionResult cmbCounty([DataSourceRequest] DataSourceRequest request, string cmbCountyName)
        {
            return Json(_dictionaryService.GetCmbCounty());
        }

        public ActionResult City_Destroy([DataSourceRequest] DataSourceRequest request, DictionaryCity dictionaryCity)
        {
            if (dictionaryCity != null)
            {
                string messageDestroyCity = _dictionaryService.City_DestroyId(dictionaryCity.CityId);

                if (string.IsNullOrWhiteSpace(messageDestroyCity))
                {
                    return Json(ModelState.ToDataSourceResult());
                }
                else
                {
                    ModelState.AddModelError("Acest oras nu poate fi sters", messageDestroyCity);
                    return Json(ModelState.ToDataSourceResult());
                }
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public IActionResult City()
        {
            return View();
        }
    }
}