using System;
using System.Collections.Generic;

#nullable disable

namespace Data.DataModels
{
    public partial class RatingType
    {
        public RatingType()
        {
            Ratings = new HashSet<Rating>();
        }

        public int RatingTypeId { get; set; }
        public string RatingName { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
