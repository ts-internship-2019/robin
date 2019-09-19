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

        public DictionaryAvailabilityController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public ActionResult DictionaryAvailability_Read([DataSourceRequest] DataSourceRequest request, string txtboxCityName)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();

            tempDataSourceResult.Total = _dictionaryService.GetDictionaryAvailabilityCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryAvailabilityFilterPage(request.Page, request.PageSize, txtboxCityName);
            return Json(tempDataSourceResult);
        }
        public ActionResult Availability_Destroy([DataSourceRequest] DataSourceRequest request, DictionaryAvailability dictionaryAvailability)
        {
            if (dictionaryAvailability != null)
            {
                string ldmType = _dictionaryService.Availability_DestroyId(dictionaryAvailability.AvailabilityId);

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
            else if (dictionaryAvailability == null)
            {

            }

            return Json(ModelState.ToDataSourceResult());
        }

        public IActionResult Availability()
        {
            return View();
        }
    }
}