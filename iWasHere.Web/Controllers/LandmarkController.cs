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
    public class LandmarkController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public LandmarkController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public ActionResult Landmark_Read([DataSourceRequest] DataSourceRequest request, string txtboxLandmarkName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();
            tempDataSourceResult.Total = _dictionaryService.GetLandmarkCount();
            tempDataSourceResult.Data = _dictionaryService.GetLandmarkPage(request.Page, request.PageSize, txtboxLandmarkName);

            return Json(tempDataSourceResult);
        }

        public IActionResult Landmark()
        {
            List<Domain.Models.Landmark> landmarks = _dictionaryService.GetLandmark();
            return View(landmarks);
        }

        public IActionResult LandmarkAdd()
        {
            return View();
        }
    }
}