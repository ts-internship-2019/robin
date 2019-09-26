using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using iWasHere.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public ActionResult CurrencyAdd(int Id)
        {
            //DictionaryCurrency dictionaryCurrency = new DictionaryCurrency { CurrencyId = 1, CurrencyName = "name" };

            //return Json(dictionaryCurrency);
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
        public ActionResult Currency_Update(int id, string currencyname, string currencycode, float conversionrate)
        {
            DictionaryCurrency dictionaryCurrency = new DictionaryCurrency();
            dictionaryCurrency.CurrencyId = id;
            dictionaryCurrency.CurrencyName = currencyname;
            dictionaryCurrency.CurrencyCode = currencycode;
            dictionaryCurrency.ConversionRate = conversionrate;
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

        public ActionResult CurrencyGetById(int txtCurrencyId)
        {

            return Json(_dictionaryService.GetDictionaryCurrencyById(txtCurrencyId));
        }

        //Problems with Json:
        public ActionResult GetDictionaryCurrencyById(int txtCurrencyId)
        {

            return Json(_dictionaryService.GetDictionaryCurrencyById(txtCurrencyId));
        }

        [HttpPost]
        public ActionResult AddNewCurrency(string currencyname, string currencycode, float conversionrate)
        {
            DictionaryCurrency dictionaryCurrency = new DictionaryCurrency
            {
                CurrencyName = currencyname,
                CurrencyCode = currencycode,
                ConversionRate = conversionrate
            };
            _dictionaryService.AddCurrency(dictionaryCurrency);

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult UpdateCurrency(int id, string name, string code, float conversionrate)
        {
            DictionaryCurrency dictionaryCurrency = new DictionaryCurrency();
            dictionaryCurrency.CurrencyId = id;
            dictionaryCurrency.CurrencyName = name;
            dictionaryCurrency.CurrencyCode = code;
            dictionaryCurrency.ConversionRate = conversionrate;
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

        public async Task<ActionResult> LoadBNRData(string DataCurs)
        {
            DateTime dt = DateTime.UtcNow;   //prima initializare
            if(!String.IsNullOrWhiteSpace(DataCurs))    //daca exista data curs
                dt = DateTime.ParseExact(DataCurs, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            CursBNRService.CursSoapClient serviciu = new CursBNRService.CursSoapClient(CursBNRService.CursSoapClient.EndpointConfiguration.CursSoap);
            CursBNRService.getallResponseGetallResult result = await serviciu.getallAsync(dt); 

            foreach (var xNode in result.Any1.Elements().Where(x => x.Value != "").FirstOrDefault().Nodes())
            {
                var elem = xNode as XElement;
                var valuta = elem.Element("IDMoneda");
                var valoare = elem.Element("Value");

                if (valuta != null && valuta.Value != null)
                    try
                    {
                        _dictionaryService.Currency_QuickUpdateCode(new DictionaryCurrency
                        {
                            ConversionRate = double.Parse(valoare.Value),
                            CurrencyCode = valuta.Value,
                            CurrencyName = valuta.Value
                        });
                    }
                    catch (DbUpdateException e)
                    {
                        throw e;
                        //var sqlEx = e.InnerException as SqlException;
                        //if (sqlEx != null && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                        //    _dictionaryService.Currency_QuickUpdateCode(new DictionaryCurrency
                        //    {
                        //        ConversionRate = double.Parse(valoare.Value),
                        //        CurrencyCode = valuta.Value,
                        //        CurrencyName = valuta.Value
                        //    });
                    }

            }
            return View("Currency");
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