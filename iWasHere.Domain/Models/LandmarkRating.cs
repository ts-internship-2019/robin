﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace iWasHere.Domain.Models
{
    public partial class LandmarkRating
    {
        public int LandmarkRatingId { get; set; }
        public int RatingValue { get; set; }
        public int LandmarkId { get; set; }
        public DateTime? ComentDate { get; set; }
        public string CommentDesc { get; set; }
        public string UserId { get; set; }
        public virtual Landmark Landmark { get; set; }
        public virtual AspNetUsers User { get; set; }

    }
}
