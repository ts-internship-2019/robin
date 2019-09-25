using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace iWasHere.Domain.Models
{
    public partial class LandmarkImage
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int LandmarkImageId { get; set; }
        public int LandmarkId { get; set; }
        public string ImageURL { get; set; }

        public virtual Landmark Landmark { get; set; }
    }
}
