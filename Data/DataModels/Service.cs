using System;
using System.Collections.Generic;

#nullable disable

namespace Data.DataModels
{
    public partial class Service
    {
        public int ServiceId { get; set; }
        public int? ServiceTypeId { get; set; }
        public int? IceCreamId { get; set; }

        public virtual IceCream IceCream { get; set; }
        public virtual ServiceType ServiceType { get; set; }
    }
}
