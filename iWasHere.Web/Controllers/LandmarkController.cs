using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iWasHere.Domain.Models;
using iWasHere.Domain.Service;
using iWasHere.Web.Models;
using iWasHere.Domain.DTOs;
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
        public ActionResult CmbCity()
        {
            return Json(_dictionaryService.GetGmbCity());
        }
        public ActionResult CmbCityByCounty(int? county)
        {
            return Json(_dictionaryService.Cascading_Get_City(county));
        }
        public ActionResult CmbCountyByCountry(int? country)
        {
            return Json(_dictionaryService.Cascading_Get_County(country));
        }
        public ActionResult CmbCurrency()
        {
            return Json(_dictionaryService.GetCmbCurrency());
        }
        public ActionResult CmbLandmarkDetail()
        {
            return Json(_dictionaryService.GetCmbLandmarkDetail());
        }
        public ActionResult CmbAttraction()
        {
            return Json(_dictionaryService.GetCmbAttraction());
        }
        public ActionResult CmbAvailability()
        {
            return Json(_dictionaryService.GeCmbAvailability());
        }

        public ActionResult GetLandmarkById(int landmarkId)
        {
            return Json(_dictionaryService.GetLandmarkById(landmarkId));
        }
        public ActionResult GetLandmarkByIdEdit(int landmarkId)
        {
            return Json(_dictionaryService.GetLandmarkByIdEdit(landmarkId));
        }
        //11 parametrii cu bilet, 8 fara
        [HttpPost]
        public ActionResult AddNewLandmark(string landmarkName, string LandmarkShortDescription, double ticketPrice, int currencyId, int ticketTypeId, int dictionaryItemId, int dictionaryAttractionTypeId, int dictionaryAvailability, int cityId, decimal longit, decimal lat)
        {
           
                Ticket ticket = new Ticket
                {
                    TicketPrice = ticketPrice,
                    TicketTypeId = ticketTypeId,
                    CurrencyId = currencyId
                };
                _dictionaryService.AddTicket(ticket);
                Landmark landmark = new Landmark
                {
                    LandmarkName = landmarkName,
                    LandmarkShortDescription = LandmarkShortDescription,
                    TicketId = ticket.TicketId,
                    DictionaryAvailabilityId = dictionaryAvailability,
                    DictionaryItemId = dictionaryItemId,
                    DictionaryAttractionTypeId = dictionaryAttractionTypeId,
                    DictionaryCityId = cityId,
                    Longitude = longit,
                    Latitude = lat,
                    DateAdded = DateTime.Now
                };
                _dictionaryService.AddLandmark(landmark);
                return Json(ModelState.ToDataSourceResult());
            
           
        }


        [HttpPost]
        public ActionResult AddNewLandmark2(string landmarkName, string LandmarkShortDescription, int dictionaryItemId, int dictionaryAttractionTypeId, int dictionaryAvailability, int cityId, decimal longit, decimal lat)
        {

            Landmark landmark = new Landmark
            {
                LandmarkName = landmarkName,
                LandmarkShortDescription = LandmarkShortDescription,
                DictionaryAvailabilityId = dictionaryAvailability,
                DictionaryItemId = dictionaryItemId,
                DictionaryAttractionTypeId = dictionaryAttractionTypeId,
                DictionaryCityId = cityId,
                Longitude = longit,
                Latitude = lat,
                DateAdded = DateTime.Now


            };
            _dictionaryService.AddLandmark(landmark);
            return Json(ModelState.ToDataSourceResult());
        }
        public ActionResult LandmarkGetById(int txtLandmarkId)
        {

            return Json(_dictionaryService.GetLandmarkById(txtLandmarkId));
        }
        public ActionResult CountyCityGetById(int txtCityId)
        {

            return Json(_dictionaryService.GetDictionaryCityCountyById(txtCityId));
        }
        public ActionResult CountryCountyGetById(int txtCountyId)
        {

            return Json(_dictionaryService.GetDictionaryCountyCountryById(txtCountyId));
        }
        public ActionResult GetCurrencyTicketType(int ticketId)
        {

            return Json(_dictionaryService.GetTicketTypeCurrency(ticketId));
        }
        [HttpPost]
        public ActionResult LandmarkUpdate(int landmarkId, string landmarkName, string LandmarkShortDescription, double ticketPrice, int currencyId, int ticketTypeId, int dictionaryItemId, int dictionaryAttractionTypeId, int dictionaryAvailability, int cityId, decimal longit, decimal lat)
        {
            Landmark landmarkWithId = _dictionaryService.GetLandmarkByIdEdit(landmarkId);
            int? ticketId= (landmarkWithId.TicketId);
            DateTime? dateAdded = landmarkWithId.DateAdded;
            int ticketId2;
            
            if (ticketId != null)
            {
                ticketId2 = Convert.ToInt32(ticketId);
                Ticket ticket = new Ticket
                {
                    TicketId = ticketId2,
                    TicketPrice = ticketPrice,
                    TicketTypeId = ticketTypeId,
                    CurrencyId = currencyId

                };
                _dictionaryService.UpdateTicket(ticket);
               
            }
            else 

            {
                Ticket ticket = new Ticket
                {
                    TicketPrice = ticketPrice,
                    TicketTypeId = ticketTypeId,
                    CurrencyId = currencyId
                };
                _dictionaryService.AddTicketLandmark(ticket);
                ticketId2 = ticket.TicketId;
               
            }
         
            Landmark landmark = new Landmark
            {
                LandmarkId = landmarkId,
                LandmarkName = landmarkName,
                LandmarkShortDescription = LandmarkShortDescription,
                TicketId = ticketId2,
                DictionaryAvailabilityId = dictionaryAvailability,
                DictionaryItemId = dictionaryItemId,
                DictionaryAttractionTypeId = dictionaryAttractionTypeId,
                DictionaryCityId = cityId,
                Longitude = longit,
                Latitude = lat,
                DateAdded = dateAdded
            };

                _dictionaryService.UpdateLandmark(landmark);


            return Json(ModelState.ToDataSourceResult());
        }

        [HttpPost]
        public ActionResult LandmarkUpdate2(int landmarkId, string landmarkName, string LandmarkShortDescription,  int dictionaryItemId, int dictionaryAttractionTypeId, int dictionaryAvailability, int cityId, decimal longit, decimal lat)
        {
            Landmark landmarkWithId = _dictionaryService.GetLandmarkByIdEdit(landmarkId);
            DateTime? dateAdded = landmarkWithId.DateAdded;

            Landmark landmark = new Landmark
            {
                LandmarkId = landmarkId,
                LandmarkName = landmarkName,
                LandmarkShortDescription = LandmarkShortDescription,
                TicketId = null,
                DictionaryAvailabilityId = dictionaryAvailability,
                DictionaryItemId = dictionaryItemId,
                DictionaryAttractionTypeId = dictionaryAttractionTypeId,
                DictionaryCityId = cityId,
                Longitude = longit,
                Latitude = lat,
                DateAdded = dateAdded
            };

            _dictionaryService.UpdateLandmark(landmark);


            return Json(ModelState.ToDataSourceResult());
        }


        public IActionResult LandmarkViewDetails()
        {
            return View(_dictionaryService.GetLandmarkReadOnly());
        }
    }
}