using System;
using System.Collections.Generic;

namespace iWasHere.Domain.Model
{
    public partial class LandmarkImage
    {
        public int LandmarkImageId { get; set; }
        public int LandmarkId { get; set; }
        public string ImageUrl { get; set; }

        public virtual Landmark Landmark { get; set; }
    }
}
