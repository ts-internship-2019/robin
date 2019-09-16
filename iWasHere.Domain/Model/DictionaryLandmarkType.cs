using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryLandmarkType
    {
        public DictionaryLandmarkType()
        {
            Landmark = new HashSet<Landmark>();
        }

        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Landmark> Landmark { get; set; }
    }
}
