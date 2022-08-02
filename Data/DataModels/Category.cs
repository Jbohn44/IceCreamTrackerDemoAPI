using System;
using System.Collections.Generic;

#nullable disable

namespace Data.DataModels
{
    public partial class Category
    {
        public Category()
        {
            IceCreamCategoryJunctions = new HashSet<IceCreamCategoryJunction>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<IceCreamCategoryJunction> IceCreamCategoryJunctions { get; set; }
    }
}
