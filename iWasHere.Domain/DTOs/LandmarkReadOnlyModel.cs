using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    class LandmarkReadOnlyModel
    {
        public string LandmarkName { get; set; }
        public string LandmarkShortDescription { get; set; }
        public string TicketCode { get; set; }
        public string TicketName { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public string AvailabilityName { get; set; }
        public string AvailabilitySchedule { get; set; }
        public string LandmarkTypeName { get; set; }
        public string LandmarkTypeCode { get; set; }
        public string DateAdded { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string CityName { get; set; }
        public string CountyName { get; set; }
        public string CountryName { get; set; }

    }
}
