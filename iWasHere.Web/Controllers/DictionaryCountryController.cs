using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
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

        public ActionResult Country_Read([DataSourceRequest] DataSourceRequest request, string txtboxCountryName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();
            tempDataSourceResult.Total = _dictionaryService.GetDictionaryCountryCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryCountryFilterPage(request.Page, request.PageSize, txtboxCountryName);
            return Json(tempDataSourceResult);
        }

        public ActionResult Country_Destroy([DataSourceRequest] DataSourceRequest request, DictionaryCountry dictionaryCountry)
        {
            if (dictionaryCountry != null)
            {
                string ldmType = _dictionaryService.AttractionType_DestroyId(dictionaryCountry.CountryId);

                if (string.IsNullOrWhiteSpace(ldmType))
                {
                    return Json(ModelState.ToDataSourceResult());
                }
                else
                {
                    ModelState.AddModelError("Error", ldmType);
                    return Json(ModelState.ToDataSourceResult());
                }
            }
            else if (dictionaryCountry == null)
            {

            }

            return Json(ModelState.ToDataSourceResult());
        }
        public IActionResult Country()
        {
            return View();
        }
    }
}