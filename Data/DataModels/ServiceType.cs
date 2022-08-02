using System;
using System.Collections.Generic;

#nullable disable

namespace Data.DataModels
{
    public partial class ServiceType
    {
        public ServiceType()
        {
            Services = new HashSet<Service>();
        }

        public int ServiceTypeId { get; set; }
        public string ServiceName { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
