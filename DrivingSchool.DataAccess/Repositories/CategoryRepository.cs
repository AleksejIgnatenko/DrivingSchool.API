﻿using DrivingSchool.Core.Models;
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

        public CategoryModel? Get(Guid id)
        {
            var categoryEntities = _context.Categories
                .FirstOrDefault(c => c.IdCategory == id);

            var category = CategoryModel.Create(categoryEntities.IdCategory, categoryEntities.NameCategory).category;

            return category;
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
