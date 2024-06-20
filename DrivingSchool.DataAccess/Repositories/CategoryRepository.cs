using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DrivingSchoolDbContext _context;

        public CategoryRepository(DrivingSchoolDbContext context)
        {
            this._context = context;
        }

        public async Task<Guid> Create(CategoryModel category)
        {
            CategoryEntity categoryEntity = new CategoryEntity
            {
                IdCategory = category.IdCategory,
                NameCategory = category.NameCategory
            };

            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();

            return categoryEntity.IdCategory;
        }

        public async Task<List<CategoryModel>> Get()
        {
            var categoryEntities = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            var categories = categoryEntities
                .Select(c => CategoryModel.Create(c.IdCategory, c.NameCategory).category)
                .ToList();

            return categories;
        }

        public async Task<CategoryModel> Get(Guid id)
        {

            var categories = await _context.Categories.FirstOrDefaultAsync(c => c.IdCategory == id);

            if (categories != null)
            {
                var category = CategoryModel.Create(categories.IdCategory, categories.NameCategory).category;
                return category;
            }
            else
            {
                throw new Exception("Категория не найдена.");
            }
        }

        public async Task<Guid> Update(Guid idCategory, string? nameCategory)
        {
            await _context.Categories
                .Where(c => c.IdCategory == idCategory)
                .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.NameCategory, nameCategory));

            return idCategory;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Categories
                .Where(c => c.IdCategory == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
