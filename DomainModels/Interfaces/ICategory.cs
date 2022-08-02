using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Interfaces
{
    public interface ICategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int UserId { get; set; }
    }
}
