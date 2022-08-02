using System;
using System.Collections.Generic;

#nullable disable

namespace Data.DataModels
{
    public partial class IceCreamImage
    {
        public int ImageId { get; set; }
        public int IceCreamId { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }

        public virtual IceCream IceCream { get; set; }
    }
}
