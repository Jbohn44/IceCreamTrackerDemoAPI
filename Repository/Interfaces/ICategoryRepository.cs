using DomainModels;
using DomainModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<ICategory>> GetAllCategories();

        Task<ICategory> AddCategory(Category category);
    }
}
