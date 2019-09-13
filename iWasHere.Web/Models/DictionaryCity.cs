﻿using System;
using System.Collections.Generic;

namespace iWasHere.Web.Models
{
    public partial class DictionaryCity
    {
        public DictionaryCity()
        {
            Landmark = new HashSet<Landmark>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int CountyId { get; set; }

        public virtual DictionaryCounty City { get; set; }
        public virtual ICollection<Landmark> Landmark { get; set; }
    }
}
