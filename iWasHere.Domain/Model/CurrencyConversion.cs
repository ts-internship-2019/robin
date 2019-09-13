using System;
using System.Collections.Generic;

namespace iWasHere.Web.Models
{
    public partial class CurrencyConversion
    {
        public int CurrencyConversionId { get; set; }
        public int CurrencyId { get; set; }
        public double? ConversionRate { get; set; }

        public virtual DictionaryCurrency Currency { get; set; }
    }
}
