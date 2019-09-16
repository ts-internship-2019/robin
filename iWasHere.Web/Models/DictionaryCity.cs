using System;
using System.Collections.Generic;

namespace iWasHere.Web.Models
{
    public partial class DictionaryCity
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountyId { get; set; }

        public virtual DictionaryCounty City { get; set; }
    }
}
