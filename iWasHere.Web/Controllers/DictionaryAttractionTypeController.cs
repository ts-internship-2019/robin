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
        private readonly RobinContext _dbContext;
        private static bool UpdateDatabase = false;

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

        public ActionResult AttractionType_Update(int id, string code, string name)
        {
            DictionaryAttractionType dictionaryAttractionType = new DictionaryAttractionType();
            dictionaryAttractionType.AttractionTypeId = id;
            dictionaryAttractionType.AttractionName = name;
            dictionaryAttractionType.AttractionCode = code;

            if (dictionaryAttractionType != null)
            {
                _dictionaryService.UpdateAttractionTypeId(dictionaryAttractionType);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult AttractionType_QuickUpdate([DataSourceRequest]DataSourceRequest request, DictionaryAttractionType dictionaryAttractionType)
        {
            if (dictionaryAttractionType != null && ModelState.IsValid)
            {
                _dictionaryService.AttractionType_QuickUpdateId(dictionaryAttractionType);
            }

            return Json(new[] { dictionaryAttractionType }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult AttractionType_Destroy([DataSourceRequest] DataSourceRequest request, DictionaryAttractionType dictionaryAttractionType)
        {
            if (dictionaryAttractionType != null)
            {
                _dictionaryService.AttractionType_DestroyId(dictionaryAttractionType.AttractionTypeId);
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

        public ActionResult AddNewAttractionType(string code, string name)
        {
            DictionaryAttractionType dictionaryAttractionType = new DictionaryAttractionType
            {
                AttractionCode = code,
                AttractionName = name,
            };
            _dictionaryService.AddNewAttractionType(dictionaryAttractionType);

            return Json(ModelState.ToDataSourceResult());
        }

        [HttpPost]
        public IActionResult AddAttractionType(DictionaryAttractionType newAttractionType)
        {
            var result = _dictionaryService.AddNewAttractionType(newAttractionType);
            return View("AttractionType");
        }

        public ActionResult AttractionTypeGetById(int txtAttractionTypeId)
        {

            return Json(_dictionaryService.GetDictionaryAttractionTypeById(txtAttractionTypeId));
        }

        public IActionResult AddAttractionType()
        {
            return View();
        }
    }
}