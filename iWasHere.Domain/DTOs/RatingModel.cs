using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class RatingModel
    {
        public int RatingValue { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

    }
}
