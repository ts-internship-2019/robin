using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Kendo.Mvc.Extensions;
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

        public ActionResult AttractionType_Read([DataSourceRequest] DataSourceRequest request, string txtboxAttractionName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();
            tempDataSourceResult.Total = _dictionaryService.GetDictionaryAttractionTypeCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryAttractionTypeFilterPage(request.Page, request.PageSize, txtboxAttractionName);
            return Json(tempDataSourceResult);
        }

        public ActionResult AttractionType_Destroy([DataSourceRequest] DataSourceRequest request, DictionaryAttractionType dictionaryAttractionType)
        {
            if (dictionaryAttractionType != null)
            {
                string ldmType = _dictionaryService.AttractionType_DestroyId(dictionaryAttractionType.AttractionTypeId);

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
            else if (dictionaryAttractionType == null)
            {

            }

            return Json(ModelState.ToDataSourceResult());
        }


        public IActionResult AttractionType()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View("AddAttractionType");
        }

        [HttpPost]
        public IActionResult AddAttraction(DictionaryAttractionType modelValuta)
        {
            var result = _dictionaryService.AddNewAttractionType(modelValuta);

            return View("AttractionType");
        }

    }
}