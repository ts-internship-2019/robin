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


        public IActionResult LandmarkDetails()
        {
            return View();
        }
    }
}