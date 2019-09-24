using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Models
{
    public partial class DictionaryCurrency
    {

        public int CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }

        public double? ConversionRate { get; set; }

        public virtual Ticket Ticket { get; set; }

    }
}
