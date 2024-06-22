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
                Id = category.Id,
                NameCategory = category.NameCategory
            };

            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();

            return categoryEntity.Id;
        }

        public async Task<List<CategoryModel>> Get()
        {
            var categoryEntities = await _context.Categories
                .AsNoTracking()
                .Include(c => c.TestEntities)
                .ToListAsync();

/*            foreach (var categoryEntity in categoryEntities) 
            {
                Console.WriteLine(categoryEntity.NameCategory);
                if (categoryEntity.NameCategory != null)
                {
                    foreach (var item in categoryEntity.TestEntities)
                    {
                        Console.WriteLine(item.NameTest);
                    }
                }
            }*/

            List<CategoryModel> categories = categoryEntities
                .Select(c => CategoryModel.Create(
                    c.Id, 
                    c.NameCategory, 
                    c.TestEntities
                        .Select(t => TestModel.Create(t.Id, CategoryModel.Create(t.Category.Id, t.Category.NameCategory).category, 
                        t.NameTest).test).ToList()).category)
                .ToList();

            return categories;
        }

        public async Task<CategoryModel> Get(Guid id)
        {

            var categories = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (categories != null)
            {
                var category = CategoryModel.Create(categories.Id, categories.NameCategory).category;
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
                .Where(c => c.Id == idCategory)
                .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.NameCategory, nameCategory));

            return idCategory;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Categories
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
