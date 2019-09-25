using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
     
        public IActionResult Currency()
        {
            return View();
        }
        public IActionResult CurrencyAdd()
        {
            return View();
        }

        public ActionResult Currency_Read([DataSourceRequest] DataSourceRequest request, string txtboxCurrencyName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();

            tempDataSourceResult.Total = _dictionaryService.GetDictionaryCurrencyCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryCurrencyFilterPage(request.Page, request.PageSize, txtboxCurrencyName);
            return Json(tempDataSourceResult);

        }

        [HttpGet]
        public IActionResult Add()
        {

            return View("AddCurrency");
        }

        [HttpPost]
        public ActionResult Currency_Update(int id, string currencyname, string currencycode)
        {
            DictionaryCurrency dictionaryCurrency = new DictionaryCurrency();
            dictionaryCurrency.CurrencyId = id;
            dictionaryCurrency.CurrencyName = currencyname;
            dictionaryCurrency.CurrencyCode = currencycode;

            if (dictionaryCurrency != null)
            {
                _dictionaryService.UpdateCurrency(dictionaryCurrency);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [HttpPost]
        public IActionResult AddValuta(DictionaryCurrency modelValuta)
        {
            var result = _dictionaryService.AdaugaValuta(modelValuta);

            return View("Index");
        }

        public ActionResult Currency_Destroy(DictionaryCurrency dictionaryCurrency)
        {
            if (dictionaryCurrency != null)
            {
                _dictionaryService.Currency_DestroyId(dictionaryCurrency.CurrencyId);
            }
            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult CountyGetById(int txtCurrencyId)
        {

            return Json(_dictionaryService.GetDictionaryCurrencyById(txtCurrencyId));
        }

        //Problems with Json:
        public ActionResult GetDictionaryCurrencyById(int txtCurrencyId)
        {

            return Json(_dictionaryService.GetDictionaryCurrencyById(txtCurrencyId));
        }

        [HttpPost]
        public ActionResult AddNewCurrency(string currencyname, string currencycode)
        {
            DictionaryCurrency dictionaryCurrency = new DictionaryCurrency
            {
                CurrencyName = currencyname,
                CurrencyCode = currencycode
            };
            _dictionaryService.AddCurrency(dictionaryCurrency);

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult UpdateCurrency(int id, string name, string code)
        {
            DictionaryCurrency dictionaryCurrency = new DictionaryCurrency();
            dictionaryCurrency.CurrencyId = id;
            dictionaryCurrency.CurrencyName = name;
            dictionaryCurrency.CurrencyCode = code;
            // dictionaryAvailability.Schedule = schedule;


            if (dictionaryCurrency != null)
            {
                _dictionaryService.UpdateCurrencyId(dictionaryCurrency);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Currency_QuickUpdateId([DataSourceRequest]DataSourceRequest request, DictionaryCurrency dictionaryCurrency)
        {
            if (dictionaryCurrency != null && ModelState.IsValid)
            {
                _dictionaryService.Currency_QuickUpdateId(dictionaryCurrency);
            }

            return Json(new[] { dictionaryCurrency }.ToDataSourceResult(request, ModelState));
        }


    }
}

//    [HttpGet]
//    public IActionResult Update()
//    {


//        return View("AddCurrency");
//    }


//    [HttpPost]
//    public IActionResult ModificaValuta(DictionaryCurrency modelValuta)
//    {
//        var result = _dictionaryService.ModificaValuta(modelValuta);

//        return View("Index");
//    }
//}