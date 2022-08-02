using System;
using System.Collections.Generic;

#nullable disable

namespace Data.DataModels
{
    public partial class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Long { get; set; }
        public int? IceCreamId { get; set; }
    }
}
