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
    public class DictionaryAvailabilityController : Controller
    {
        private readonly DictionaryService _dictionaryService;
        private readonly RobinContext _dbContext;
        private static bool UpdateDatabase = false;

        public DictionaryAvailabilityController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public ActionResult DictionaryAvailability_Read([DataSourceRequest] DataSourceRequest request, string txtboxAvailabilityName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();

            tempDataSourceResult.Total = _dictionaryService.GetDictionaryAvailabilityCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryAvailabilityFilterPage(request.Page, request.PageSize, txtboxAvailabilityName);
            return Json(tempDataSourceResult);
        }
        public ActionResult DictionaryAvailability_Update(int id, string name)
        {
            DictionaryAvailability dictionaryAvailability = new DictionaryAvailability();
            dictionaryAvailability.AvailabilityId = id;
            dictionaryAvailability.AvailabilityName = name;
           // dictionaryAvailability.Schedule = schedule;


            if (dictionaryAvailability != null)
            {
                _dictionaryService.UpdateAvailabilityId(dictionaryAvailability);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult Availability_QuickUpdate([DataSourceRequest]DataSourceRequest request, DictionaryAvailability dictionaryAvailability)
        {
            if (dictionaryAvailability != null && ModelState.IsValid)
            {
                _dictionaryService.Availability_QuickUpdateId(dictionaryAvailability);
            }

            return Json(new[] { dictionaryAvailability }.ToDataSourceResult(request, ModelState));
        }





        public ActionResult Availability_Destroy([DataSourceRequest] DataSourceRequest request, DictionaryAvailability dictionaryAvailability)
        {
            if (dictionaryAvailability != null)
            {
                _dictionaryService.Availability_DestroyId(dictionaryAvailability.AvailabilityId);
            }
            return Json(ModelState.ToDataSourceResult());
        }




        public IActionResult Availability()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddNewAvailability( string name)
        {
            DictionaryAvailability dictionaryAvailability = new DictionaryAvailability
            {
                AvailabilityName = name,
            };
            _dictionaryService.AddNewAvailability(dictionaryAvailability);

            return Json(ModelState.ToDataSourceResult());
        }


        [HttpPost]
        public IActionResult AddAvailability(DictionaryAvailability newAvailability)
        {
            var result = _dictionaryService.AddNewAvailability(newAvailability);
            return View("Availability");
        }

        public ActionResult AvailabilityGetById(int txtAvailabilityId)
        {

            return Json(_dictionaryService.GetDictionaryAvailabilityById(txtAvailabilityId));
        }


        public IActionResult AddAvailability()
        {
            return View();
        }
    }
}