using System;
using System.Collections.Generic;

#nullable disable

namespace Data.DataModels
{
    public partial class User
    {
        public User()
        {
            Categories = new HashSet<Category>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ExternalId { get; set; }
        public string Email { get; set; }
        public string Provider { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}
