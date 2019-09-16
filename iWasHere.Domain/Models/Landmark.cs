using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Landmark
    {
        public Landmark()
        {
            LandmarkImage = new HashSet<LandmarkImage>();
            LandmarkRating = new HashSet<LandmarkRating>();
        }

        public int LandmarkId { get; set; }
        public string LandmarkName { get; set; }
        public string LandmarkShortDescription { get; set; }
        public int? TicketId { get; set; }
        public int? DictionaryAvailabilityId { get; set; }
        public int? DictionaryItemId { get; set; }
        public DateTime? DateAdded { get; set; }
        public int? DictionaryAttractionTypeId { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public int? DictionaryCityId { get; set; }

        public virtual DictionaryAttractionType DictionaryAttractionType { get; set; }
        public virtual DictionaryAvailability DictionaryAvailability { get; set; }
        public virtual DictionaryCity DictionaryCity { get; set; }
        public virtual DictionaryLandmarkType DictionaryItem { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual ICollection<LandmarkImage> LandmarkImage { get; set; }
        public virtual ICollection<LandmarkRating> LandmarkRating { get; set; }
    }
}
