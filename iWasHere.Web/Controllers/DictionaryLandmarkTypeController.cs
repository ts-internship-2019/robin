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

namespace iWasHere.Web.Controllers
{
    public class DictionaryLandmarkTypeController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryLandmarkTypeController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }


        public ActionResult LandmarkType_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();

            tempDataSourceResult.Total = _dictionaryService.GetDictionaryLandmarkTypeCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryLandmarkTypePage(request.Page, request.PageSize);
            return Json(tempDataSourceResult);
        }

        //public ActionResult LandmarkType_Create(int id, [DataSourceRequest] DataSourceRequest request, DictionaryLandmarkType dictionaryLandmarkType)
        //{
        //    if (dictionaryLandmarkType != null && ModelState.IsValid)
        //    {
        //        IList<DictionaryLandmarkType> dlc = getRouteCustomersFromSession();
        //        dlc.Add(dictionaryLandmarkType);
        //    }

        //    return Json(new[] { dictionaryLandmarkType }.ToDataSourceResult(request, ModelState));
        //}

        //--------------------

        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LandmarkType_Create([DataSourceRequest] DataSourceRequest request, DictionaryLandmarkType dictionaryLandmarkType)
        {
            if (dictionaryLandmarkType != null && ModelState.IsValid)
            {
                //DictionaryService.LandmarkType_Create(dictionaryLandmarkType);
            }

            return Json(new[] { dictionaryLandmarkType }.ToDataSourceResult(request, ModelState));
        }


        public IActionResult LandmarkDetails()
        {
            return View();
        }


        public IActionResult AddLandmarkDetails()
        {
            return View();
        }
    }
}