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

        public ActionResult Country_Update(int id, string name)
        {
            DictionaryCountry dictionaryCountry = new DictionaryCountry();
            dictionaryCountry.CountryId = id;
            dictionaryCountry.CountryName = name;

            if (dictionaryCountry != null)
            {
                _dictionaryService.UpdateCountryId(dictionaryCountry);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Country_QuickUpdate([DataSourceRequest]DataSourceRequest request, DictionaryCountry dictionaryCountry)
        {
            if (dictionaryCountry != null && ModelState.IsValid)
            {
                _dictionaryService.Country_QuickUpdateId(dictionaryCountry);
            }

            return Json(new[] { dictionaryCountry }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Country_Destroy([DataSourceRequest] DataSourceRequest request, DictionaryCountry dictionaryCountry)
        {
            if (dictionaryCountry != null)
            {
                _dictionaryService.Country_DestroyId(dictionaryCountry.CountryId);
            }
            return Json(ModelState.ToDataSourceResult());
        }


        public IActionResult Country()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddNewCountry(string name)
        {
            DictionaryCountry dictionaryCountry = new DictionaryCountry
            {
                CountryName = name,

            };
            _dictionaryService.AddNewCountry(dictionaryCountry);

            return Json(ModelState.ToDataSourceResult());
        }


        [HttpPost]
        public IActionResult AddCountry(DictionaryCountry dictionaryCountry)
        {
            var result = _dictionaryService.AddNewCountry(dictionaryCountry);
            return View("AddCountry");
        }

        public ActionResult CountryGetById(int txtCountryId)
        {

            return Json(_dictionaryService.GetDictionaryCountryById(txtCountryId));
        }

        public IActionResult AddCountry()
        {
            return View();
        }
    }
}