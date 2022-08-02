using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModels
{
    public class IceCream
    {
        public int IceCreamId { get; set; }
        public int UserId { get; set; }
        public string Location { get; set; }
        public string Business { get; set; }
        public string FlavorName { get; set; }
        public Rating OverAllRating { get; set; }
        public Rating FlavorRating { get; set; }
        public Rating CreaminessRating { get; set; }
        public Rating IcinessRating { get; set; }
        public Rating DensityRating { get; set; }
        public Rating ValueRating { get; set; }
        public List<DomainModels.Service> Services { get; set; }
        public string Comments { get; set; }
        public DateTime? ReviewDate { get; set; }
        public int? CategoryId { get; set; }
    }
}
