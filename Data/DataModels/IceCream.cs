using System;
using System.Collections.Generic;

#nullable disable

namespace Data.DataModels
{
    public partial class IceCream
    {
        public IceCream()
        {
            IceCreamCategoryJunctions = new HashSet<IceCreamCategoryJunction>();
            IceCreamImages = new HashSet<IceCreamImage>();
            Ratings = new HashSet<Rating>();
            Services = new HashSet<Service>();
        }

        public int IceCreamId { get; set; }
        public int UserId { get; set; }
        public string FlavorName { get; set; }
        public string Location { get; set; }
        public string Business { get; set; }
        public string Comments { get; set; }
        public DateTime? ReviewDate { get; set; }

        public virtual ICollection<IceCreamCategoryJunction> IceCreamCategoryJunctions { get; set; }
        public virtual ICollection<IceCreamImage> IceCreamImages { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
