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

        public ActionResult TicketTypeRead([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult tempDataSourceResult = new DataSourceResult();
            tempDataSourceResult.Total = _dictionaryService.GetDictionaryTicketTypeCount();
            tempDataSourceResult.Data = _dictionaryService.GetDictionaryTicketTypePage(request.Page, request.PageSize);

            return Json(tempDataSourceResult);
        }

    }
}