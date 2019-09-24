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

        public ActionResult County_Read([DataSourceRequest] DataSourceRequest request, string txtboxCountyName,int cmbboxCountryName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();
            tempDataSourceResult.Total = _dictionaryService.GetDictionaryCountyCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryCountyPage(request.Page,request.PageSize, txtboxCountyName, cmbboxCountryName);

            return Json(tempDataSourceResult);
        }

        public ActionResult cmbCountry([DataSourceRequest] DataSourceRequest request, string cmbCountryName)
        {
            return Json(_dictionaryService.GetCmbCountry());
        }
        [HttpPost]
        public ActionResult County_Update(int id, string countyname, int countryid)
        {
            DictionaryCounty dictionaryCounty = new DictionaryCounty();
            dictionaryCounty.CountyId = id;
            dictionaryCounty.CountyName = countyname;
            dictionaryCounty.CountryId = countryid;

            if (dictionaryCounty != null)
            {
                _dictionaryService.UpdateCounty(dictionaryCounty);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult County_Destroy(DictionaryCounty dictionaryCounty)
        {
            if (dictionaryCounty != null)
            {
                _dictionaryService.County_DestroyId(dictionaryCounty.CountyId);
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public IActionResult County()
        {
            return View();
        }

        public IActionResult CountyAdd()
        {
            return View();
        }

        public ActionResult CountyGetById(int txtCountyId)
        {

            return Json(_dictionaryService.GetDictionaryCountyById(txtCountyId));
        }

        [HttpPost]
        public ActionResult AddNewCounty(string countyname, int countryid)
        {
            DictionaryCounty dictionaryCounty = new DictionaryCounty
            {
                CountyName = countyname,
                CountryId = countryid
            };
            _dictionaryService.AddCounty(dictionaryCounty);

            return Json(ModelState.ToDataSourceResult());
        }

    }
}