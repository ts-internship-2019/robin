using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryAvailability
    {
        public DictionaryAvailability()
        {
            Landmark = new HashSet<Landmark>();
        }

        public int AvailabilityId { get; set; }
        public string AvailabilityName { get; set; }
        public string Schedule { get; set; }

        public virtual ICollection<Landmark> Landmark { get; set; }
    }
}
