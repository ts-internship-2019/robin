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

        public  ActionResult TicketTypeGetById([DataSourceRequest] DataSourceRequest request, string txtTicketTypeId)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryTicketTypeById(txtTicketTypeId);
            return Json(tempDataSourceResult);
        }
        public IActionResult TicketAdd()
        {
            return View();
        }

    }
}