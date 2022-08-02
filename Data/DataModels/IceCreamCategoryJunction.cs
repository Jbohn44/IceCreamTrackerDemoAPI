using System;
using System.Collections.Generic;

#nullable disable

namespace Data.DataModels
{
    public partial class IceCreamCategoryJunction
    {
        public int IceCreamCategoryId { get; set; }
        public int IceCreamId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual IceCream IceCream { get; set; }
    }
}
