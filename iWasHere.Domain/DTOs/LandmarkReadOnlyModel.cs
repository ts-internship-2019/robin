using iWasHere.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class LandmarkReadOnlyModel
    {
        public string LandmarkName { get; set; }
        public string LandmarkShortDescription { get; set; }
        public string TicketCode { get; set; }
        public double? TicketPrice {get;set;}
        public string TicketName { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public double? CurrencyRate { get; set; }
        public string AvailabilityName { get; set; }
        public string AvailabilitySchedule { get; set; }
        public string LandmarkTypeName { get; set; }
        public string LandmarkTypeCode { get; set; }
        public DateTime? DateAdded { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string CityName { get; set; }
        public string CountyName { get; set; }
        public string CountryName { get; set; }

        public string AttractionName { get; set; }
        public string AttractionCode { get; set; }
        /// <summary>
        /// /COMENTARII
        /// </summary>
        public string Comment { get; set; }
        public DateTime? CommentDate { get; set; }
        public int RatingValue { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }



        public LandmarkReadOnlyModel ConvertToModel(Landmark landmark,LandmarkRating landmarkRating)
        {
            LandmarkReadOnlyModel temp = new LandmarkReadOnlyModel();
            if (landmark.LandmarkName != null)
                temp.LandmarkName = landmark.LandmarkName;
            else
                temp.LandmarkName = "NECOMPLETAT";
            if (landmark.LandmarkShortDescription != null)
                temp.LandmarkShortDescription = landmark.LandmarkShortDescription;
            else
                temp.LandmarkShortDescription = "NECOMPLETAT";
            if (landmark.DictionaryItem.ItemCode != null)
                temp.LandmarkTypeCode = landmark.DictionaryItem.ItemCode;
            else
                temp.LandmarkTypeCode = "NECOMPLETAT";
            if (landmark.DictionaryItem.ItemName != null)
                temp.LandmarkTypeName = landmark.DictionaryItem.ItemName;
            else
                temp.LandmarkTypeName = "NECOMPLETAT";
            if (landmark.DictionaryAttractionType.AttractionName != null)
                temp.AttractionName = landmark.DictionaryAttractionType.AttractionName;
            else
                temp.AttractionName = "NECOMPLETAT";
            if (landmark.DictionaryAttractionType.AttractionCode != null)
                temp.AttractionCode = landmark.DictionaryAttractionType.AttractionCode;
            else
                temp.AttractionCode = "NECOMPLETAT";
            if (landmark.Latitude!=null)
                temp.Latitude = landmark.Latitude;
            else
                temp.Latitude = -1;
            if (landmark.Longitude != null)
                temp.Longitude = landmark.Longitude;
            else
                temp.Longitude = -1;
            if (landmark.Ticket != null)
                temp.TicketPrice = landmark.Ticket.TicketPrice;
            else
                temp.TicketPrice = -1;
            if (landmark.Ticket!= null)
                if(landmark.Ticket.TicketType!=null)
                    temp.TicketCode = landmark.Ticket.TicketType.TicketCode;
                else
                    temp.TicketCode = "NECOMPLETAT";
            else
                temp.TicketCode = "NECOMPLETAT";
            if (landmark.Ticket != null)
                if (landmark.Ticket.TicketType != null)
                    temp.TicketName = landmark.Ticket.TicketType.TicketName;
                else
                    temp.TicketName = "NECOMPLETAT";
            else
                temp.TicketName = "NECOMPLETAT";
            if (landmark.DateAdded != null)
                temp.DateAdded = landmark.DateAdded;
            else
                temp.DateAdded = DateTime.Now;
            if (landmark.Ticket!=null)
                if(landmark.Ticket.DictionaryCurrency != null)
                    temp.CurrencyCode = landmark.Ticket.DictionaryCurrency.CurrencyCode;
                else
                    temp.CurrencyCode = "NECOMPLETAT";
            else
                temp.CurrencyCode = "NECOMPLETAT";
            if (landmark.Ticket != null)
                if (landmark.Ticket.DictionaryCurrency != null)
                    temp.CurrencyName = landmark.Ticket.DictionaryCurrency.CurrencyName;
                else
                    temp.CurrencyName = "NECOMPLETAT";
            else
                temp.CurrencyName = "NECOMPLETAT";
            if (landmark.Ticket != null)
                if (landmark.Ticket.DictionaryCurrency != null)
                    temp.CurrencyRate = landmark.Ticket.DictionaryCurrency.ConversionRate;
                else
                    temp.CurrencyRate = -1;
            else
                temp.CurrencyRate = -1;
            if (landmark.DictionaryAvailability != null)
                temp.AvailabilityName = landmark.DictionaryAvailability.AvailabilityName;
            else
                temp.AvailabilityName = "NECOMPLETAT";
            if (landmark.DictionaryAvailability != null)
                temp.AvailabilitySchedule = landmark.DictionaryAvailability.Schedule;
            else
                temp.AvailabilitySchedule = "NECOMPLETAT";
            if (landmark.DictionaryCity.CityName != null)
                temp.CityName = landmark.DictionaryCity.CityName;
            else
                temp.CityName = "NECOMPLETAT";
            if (landmark.DictionaryCity.County.CountyName != null)
                temp.CountyName = landmark.DictionaryCity.County.CountyName;
            else
                temp.CountyName = "NECOMPLETAT";
            if (landmark.DictionaryCity.County.Country.CountryName!=null)
                temp.CountryName = landmark.DictionaryCity.County.Country.CountryName;
            else
                temp.CountryName = "NECOMPLETAT";

            if (landmarkRating != null)
            {
                temp.Comment = landmarkRating.CommentDesc;
                temp.RatingValue = landmarkRating.RatingValue;
                temp.CommentDate = landmarkRating.ComentDate;
                if(landmarkRating.User!=null)
                    temp.UserId = landmarkRating.User.Id;
                else
                    temp.UserId = "df25da26-9fbd-4888-b1b8-d1905fce6465";// "Anonim@totalsoft.ro";
            }
            else
            {
                temp.CommentDate = DateTime.Now;
                temp.RatingValue = -1;
                temp.UserId = "df25da26-9fbd-4888-b1b8-d1905fce6465";// "Anonim@totalsoft.ro";
                temp.Comment = "NECOMPLETAT";
            }
            return temp;
        }
    }
}
