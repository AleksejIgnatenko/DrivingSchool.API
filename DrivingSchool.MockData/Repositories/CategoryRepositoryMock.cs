﻿using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.MockData.Repositories
{
    public class CategoryRepositoryMock
    {
        public void Dispose()
        {
        }

        public async Task<Guid> CreateAsync(CategoryModel category, CancellationToken ct = default)
        {
            CategoryEntity categoryEntity = new CategoryEntity
            {
                Id = category.Id,
                NameCategory = category.NameCategory
            };

            return await Task.FromResult(categoryEntity.Id);
        }

        public async Task<List<CategoryModel>> GetAsync(CancellationToken ct = default)
        {
            var categoryEntity = new List<CategoryEntity>
            {
                new CategoryEntity
                {
                    Id = Guid.NewGuid(),
                    NameCategory = "Test category",
                }
            };

            var testEntity = new List<TestEntity>
            {
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    Category = categoryEntity[0],
                    NameTest = "Test test"
                }
            };

            categoryEntity[0].Tests = testEntity;

            List<CategoryModel> categories = categoryEntity
                .Select(c => CategoryModel.Create(
                    c.Id,
                    c.NameCategory,
                    c.Tests
                        .Select(t => TestModel.Create(
                            t.Id,
                            CategoryModel.Create(t.Category.Id, t.Category.NameCategory).category,
                            t.NameTest).test)
                        .ToList())
                    .category)
                .ToList();

            return await Task.FromResult(categories);
        }

        public async Task<CategoryModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var categoryEntities = new List<CategoryEntity>
            {
                new CategoryEntity
                {
                    Id = Guid.Parse("d833d875-660f-4d6b-a138-796a6ae98095"),
                    NameCategory = "Test category",
                }
            };

            var testEntity = new List<TestEntity>
            {
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    Category = categoryEntities[0],
                    NameTest = "Test test"
                }
            };

            categoryEntities[0].Tests = testEntity;

            var categoryEntity = categoryEntities
                .FirstOrDefault(c => c.Id == id);

            if (categoryEntity != null)
            {
                var categoryModel = CategoryModel.Create(
                    categoryEntity.Id,
                    categoryEntity.NameCategory,
                    categoryEntity.Tests
                        .Select(t => TestModel.Create(
                            t.Id,
                            CategoryModel.Create(t.Category.Id, t.Category.NameCategory).category,
                            t.NameTest).test)
                        .ToList()
                    ).category;

                return await Task.FromResult(categoryModel);
            }

            throw new Exception("Категория не найдена.");
        }

        public async Task<Guid> UpdateAsync(Guid idCategory, string? nameCategory, CancellationToken ct = default)
        {
            return await Task.FromResult(idCategory);
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            return await Task.FromResult(id);
        }
    }
}
