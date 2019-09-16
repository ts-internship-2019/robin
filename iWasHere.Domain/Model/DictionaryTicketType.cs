using System;
using System.Collections.Generic;

namespace iWasHere.Web.Models
{
    public partial class DictionaryTicketType
    {
        public DictionaryTicketType()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int TicketTypeId { get; set; }
        public string TicketCode { get; set; }
        public string TicketName { get; set; }

        public virtual Ticket TicketType { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
