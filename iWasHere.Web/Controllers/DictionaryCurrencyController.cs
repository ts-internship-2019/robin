﻿using System;
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
    public class DictionaryCurrencyController : Controller
    {
        private readonly DictionaryService _dictionaryService;

        public DictionaryCurrencyController(DictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public ActionResult Currency_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_dictionaryService.GetDictionaryCurrency().ToDataSourceResult(request));
        }

        public IActionResult Currency()
        {
            List<Domain.Models.DictionaryCurrency> dictionaryCurrency = _dictionaryService.GetDictionaryCurrency();

            return View(dictionaryCurrency);
        }
    }
}