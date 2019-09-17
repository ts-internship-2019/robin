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
           // List<DictionaryTicketTypeModel> dictionaryTicketTypeModels = _dictionaryService.GetDictionaryTicketType();
            List<DictionaryTicketType> dictionaryTicketType = _dictionaryService.GetDictionaryTicketType();
            return View(dictionaryTicketType);
        }

        public ActionResult TicketTypeRead([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionaryTicketType().ToDataSourceResult(request));
        }

    }
}