using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;
using DrivingSchool.Infrastructure.CustomException;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DrivingSchoolDbContext _context;

        public CategoryRepository(DrivingSchoolDbContext context)
        {
            this._context = context;
        }

        public async Task<Guid> CreateAsync(CategoryModel category)
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

        public async Task<List<CategoryModel>> GetAsync()
        {
            var categoryEntities = await _context.Categories
                .AsNoTracking()
                .Include(c => c.Tests)
                .ToListAsync();

            List<CategoryModel> categories = categoryEntities
                .Select(c => CategoryModel.Create(
                    c.Id,
                    c.NameCategory,
                    c.Tests
                        .Select(t => TestModel.Create(t.Id, CategoryModel.Create(t.Category.Id, t.Category.NameCategory).category,
                        t.NameTest).test).ToList()).category)
                .ToList();

            return categories;
        }

        public async Task<CategoryModel> GetByIdAsync(Guid id)
        {
            var categories = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (categories != null)
            {
                var category = CategoryModel.Create(categories.Id, categories.NameCategory).category;
                return category;
            }
            throw new CustomException("Категория не найдена.");
        }

        public async Task<CategoryModel> UpdateAsync(Guid idCategory, string? nameCategory)
        {
            await _context.Categories
                .Where(c => c.Id == idCategory)
                .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.NameCategory, nameCategory));

            var categoryEntity = await _context.Categories.FirstOrDefaultAsync(c => c.Id == idCategory);
            if (categoryEntity != null)
            {
                var category = CategoryModel.Create(categoryEntity.Id, categoryEntity.NameCategory).category;
                return category;
            }
            throw new CustomException("Не удалось обновить категорию");
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            await _context.Categories
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
