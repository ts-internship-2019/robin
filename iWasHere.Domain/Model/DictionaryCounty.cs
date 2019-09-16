using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class DictionaryCounty
    {
        public int CountyId { get; set; }
        public string CountyName { get; set; }
        public int CountryId { get; set; }

        public virtual DictionaryCountry Country { get; set; }
        public virtual DictionaryCity DictionaryCity { get; set; }
    }
}
