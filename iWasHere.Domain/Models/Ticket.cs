﻿using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            Landmark = new HashSet<Landmark>();
        }

        public int TicketId { get; set; }
        public double? TicketPrice { get; set; }
        public int CurrencyId { get; set; }
        public int TicketTypeId { get; set; }

        public virtual DictionaryCurrency DictionaryCurrency { get; set; }
        public virtual DictionaryTicketType TicketType { get; set; }
        public virtual ICollection<Landmark> Landmark { get; set; }
    }
}
