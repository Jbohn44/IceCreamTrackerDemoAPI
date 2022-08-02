using System;
using System.Collections.Generic;

#nullable disable

namespace Data.DataModels
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int IceCreamId { get; set; }
        public int RatingTypeId { get; set; }
        public int RatingValue { get; set; }

        public virtual IceCream IceCream { get; set; }
        public virtual RatingType RatingType { get; set; }
    }
}
