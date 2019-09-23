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
        //added 18 sep pentru butonul de ADD, am facut new folder cu Add si Index in DictionaryCurrency
        public IActionResult Index()
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
        public IActionResult AddValuta(DictionaryCurrency modelValuta)
        {
            var result = _dictionaryService.AdaugaValuta(modelValuta);

            return View("Index");
        }

        public ActionResult Currency_Destroy([DataSourceRequest] DataSourceRequest request, DictionaryCurrency dictionaryCurrency)
        {
            if (dictionaryCurrency != null)
            {
                string CurrencyType = _dictionaryService.Currency_DestroyId(dictionaryCurrency.CurrencyId);

                if (string.IsNullOrWhiteSpace(CurrencyType))
                {
                    return Json(ModelState.ToDataSourceResult());
                }
                else
                {
                    ModelState.AddModelError("Error", CurrencyType);
                    return Json(ModelState.ToDataSourceResult());
                }
            }
            else if (dictionaryCurrency != null)
            { }

            return Json(ModelState.ToDataSourceResult());
        }





        //Problems with Json:
        public ActionResult GetDictionaryCurrencyById(int txtCurrencyId)
        {

            return Json(_dictionaryService.GetDictionaryCurrencyById(txtCurrencyId));
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