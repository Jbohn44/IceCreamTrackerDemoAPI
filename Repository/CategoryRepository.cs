using Data.DataModels;
using DomainModels.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private IceCreamDataContext _context;

        public CategoryRepository(IceCreamDataContext context)
        {
            _context = context;
        }
        public async Task<List<ICategory>> GetAllCategories()
        {
            //this might get out of hand if categories grows too large
            var categories = await _context.Categories.ToListAsync();
            return categories.Select(Map).ToList();
        }
        
        public async Task<ICategory> AddCategory(DomainModels.Category category)
        {
            var categoryDataModel = new Data.DataModels.Category
            {
                CategoryName = category.CategoryName.Trim(),
                UserId = category.UserId
            };
            await _context.Categories.AddAsync(categoryDataModel);
            await _context.SaveChangesAsync();
            return Map(categoryDataModel);
        }
        public ICategory Map(Data.DataModels.Category category)
        {
            return new DomainModels.Category
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                UserId = category.UserId
            };
        }

       
    }
}
