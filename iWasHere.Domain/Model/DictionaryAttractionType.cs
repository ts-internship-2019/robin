using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryAttractionType
    {
        public DictionaryAttractionType()
        {
            Landmark = new HashSet<Landmark>();
        }

        public int AttractionTypeId { get; set; }
        public string AttractionCode { get; set; }
        public string AttractionName { get; set; }

        public virtual ICollection<Landmark> Landmark { get; set; }
    }
}
