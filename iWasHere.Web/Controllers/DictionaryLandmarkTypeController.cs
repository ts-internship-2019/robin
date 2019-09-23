using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.DTOs;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using iWasHere.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace iWasHere.Web.Controllers
{
    public class DictionaryLandmarkTypeController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryLandmarkTypeController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }


        public ActionResult LandmarkType_Read([DataSourceRequest] DataSourceRequest request, string txtboxItemName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();

            tempDataSourceResult.Total = _dictionaryService.GetDictionaryLandmarkTypeCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryLandmarkTypeFilterPage(request.Page, request.PageSize, txtboxItemName);
            return Json(tempDataSourceResult);
        }




        //public ActionResult LandmarkType_Update([DataSourceRequest] DataSourceRequest request, DictionaryLandmarkType dictionaryLandmarkType)
        //{
        //    if (dictionaryLandmarkType != null && ModelState.IsValid)
        //    {
        //        _dictionaryService.LandmarkType_UpdateId(dictionaryLandmarkType);
        //    }

        //    return Json(new[] { dictionaryLandmarkType }.ToDataSourceResult(request, ModelState));
        //}


        public ActionResult LandmarkType_Update(int id, string code, string name, string description)
        {
            DictionaryLandmarkType dictionaryLandmarkType = new DictionaryLandmarkType();
            dictionaryLandmarkType.ItemId = id;
            dictionaryLandmarkType.ItemName = name;
            dictionaryLandmarkType.ItemCode = code;
            dictionaryLandmarkType.Description = description;

            if (dictionaryLandmarkType != null)
            {
                _dictionaryService.UpdateLandmarkTypeId(dictionaryLandmarkType);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult LandmarkType_QuickUpdate([DataSourceRequest]DataSourceRequest request, DictionaryLandmarkType dictionaryLandmarkType)
        {
            if (dictionaryLandmarkType != null && ModelState.IsValid)
            {
                _dictionaryService.LandmarkType_QuickUpdateId(dictionaryLandmarkType);
            }

            return Json(new[] { dictionaryLandmarkType }.ToDataSourceResult(request, ModelState));
        }





        public ActionResult LandmarkType_Destroy([DataSourceRequest] DataSourceRequest request, DictionaryLandmarkType dictionaryLandmarkType)
        {
            if (dictionaryLandmarkType != null)
            {
                _dictionaryService.LandmarkType_DestroyId(dictionaryLandmarkType.ItemId);
            }
            return Json(ModelState.ToDataSourceResult());
        }




        public IActionResult LandmarkDetails()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Add()
        {

            return View("AddLandmarkDetails");
        }


        [HttpPost]
        public ActionResult AddNewLandmarkType(string code, string name, string description)
        {
            DictionaryLandmarkType dictionaryLandmarkType = new DictionaryLandmarkType
            {
                ItemCode = code,
                ItemName = name,
                Description = description
            };
            _dictionaryService.AddNewLandmarkDetails(dictionaryLandmarkType);

            return Json(ModelState.ToDataSourceResult());
        }


        [HttpPost]
        public IActionResult AddLandmarkDetails(DictionaryLandmarkType newLandmarkType)
        {
            var result = _dictionaryService.AddNewLandmarkDetails(newLandmarkType);
            return View("LandmarkDetails");
        }

        public ActionResult LandmarkTypeGetById(int txtLandmarkTypeId)
        {

            return Json(_dictionaryService.GetDictionaryLandmarkTypeById(txtLandmarkTypeId));
        }


        public IActionResult AddLandmarkDetails()
        {
            return View();
        }
    }
}