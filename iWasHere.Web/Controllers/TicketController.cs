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
    public class TicketController : Controller
    {  private readonly DictionaryService _dictionaryService;
        public TicketController (DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public IActionResult Ticket()
        {
            return View();
        }

        public ActionResult TicketTypeRead([DataSourceRequest] DataSourceRequest request, string txtTicketTypeCode)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();
            tempDataSourceResult.Total = _dictionaryService.GetDictionaryTicketTypeCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryTicketTypeFilterPage(request.Page, request.PageSize, txtTicketTypeCode);

            return Json(tempDataSourceResult);
        }

        public ActionResult TicketTypeGetById(int txtTicketTypeId)
        {
      
            return Json(_dictionaryService.GetDictionaryTicketTypeById(txtTicketTypeId));
        }

        [HttpPost]
        public ActionResult TicketType_Update(int id,string code,string name)
        {
            DictionaryTicketType dictionaryTicketType = new DictionaryTicketType();
            dictionaryTicketType.TicketTypeId = id;
            dictionaryTicketType.TicketName = name;
            dictionaryTicketType.TicketCode = code;

            if (dictionaryTicketType != null)
            {
                _dictionaryService.UpdateTicketTypeId(dictionaryTicketType);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        [HttpPost]
        public ActionResult AddNewTicketType( string code, string name)
        {
            DictionaryTicketType dictionaryTicketType = new DictionaryTicketType
            {
                TicketName = name,
                TicketCode = code
            };
            _dictionaryService.AddTicketType(dictionaryTicketType);

            return Json(ModelState.ToDataSourceResult());
        }

        public ActionResult TicketType_Destroy([DataSourceRequest] DataSourceRequest request, DictionaryTicketType dictionaryTicketType)
        {

            string error = _dictionaryService.TicketType_DestroyId(dictionaryTicketType.TicketTypeId);
            if (!String.IsNullOrEmpty(error))
            {
                ModelState.AddModelError("e", error);
                return Json(ModelState.ToDataSourceResult());
            }
            else
            {
                if (dictionaryTicketType != null)
                {
                    _dictionaryService.TicketType_DestroyId(dictionaryTicketType.TicketTypeId);
                }
                return Json(ModelState.ToDataSourceResult());

            }
        }
        public ActionResult CmbTicketType([DataSourceRequest] DataSourceRequest request, string cmbTicketTypeName)
        {
            return Json(_dictionaryService.GetCmbTicketType());
        }

        public IActionResult TicketAdd()
        {
            return View();
        }

    }
}