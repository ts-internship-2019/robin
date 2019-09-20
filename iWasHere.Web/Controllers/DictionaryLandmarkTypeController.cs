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
        private readonly RobinContext _dbContext;
        private static bool UpdateDatabase = false;

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

       

        

        public ActionResult LandmarkType_Destroy([DataSourceRequest] DataSourceRequest request, DictionaryLandmarkType dictionaryLandmarkType)
        {
            if (dictionaryLandmarkType != null)
            {
                string ldmType = _dictionaryService.LandmarkType_DestroyId(dictionaryLandmarkType.ItemId);

                if(string.IsNullOrWhiteSpace(ldmType))
                {
                    return Json(ModelState.ToDataSourceResult());
                }
                else
                {
                    ModelState.AddModelError("Error", ldmType);
                    return Json(ModelState.ToDataSourceResult());
                }
            }
            else if (dictionaryLandmarkType == null)
            {

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
        public IActionResult AddLandmarkDetails(DictionaryLandmarkType newLandmarkType)
        {
            var result = _dictionaryService.AddNewLandmarkDetails(newLandmarkType);
            return View("LandmarkDetails");
        }
    }
}