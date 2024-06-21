using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DrivingSchool.DataAccess.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly DrivingSchoolDbContext _context;

        public TestRepository(DrivingSchoolDbContext context)
        {
            this._context = context;
        }

        public async Task<Guid> Create(TestModel test)
        {
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == test.Category.Id);

            CategoryEntity categoryEntity;

            if (existingCategory != null)
            {
                categoryEntity = existingCategory;
            }
            else
            {
                categoryEntity = new CategoryEntity
                {
                    Id = test.Category.Id,
                    NameCategory = test.Category.NameCategory
                };
            }

            TestEntity testEntity = new TestEntity
            {
                Id = test.Id,
                Category = categoryEntity,
                NameTest = test.NameTest
            };

            await _context.Tests.AddAsync(testEntity);
            await _context.SaveChangesAsync();

            return testEntity.Id;
        }

        public async Task<List<TestModel>> Get()
        {
            var testEntity = await _context.Tests
                .AsNoTracking()
                .ToListAsync();

            var tests = testEntity
                .Select(t => TestModel.Create(t.Id, (CategoryModel.Create(t.Category.Id, t.Category.NameCategory).category), t.NameTest).test)
                .ToList();

            return tests;
        }

        public TestModel? GetById(Guid id)
        {
            var testEntity = _context.Tests
                .FirstOrDefault(t => t.Id == id);

            if (testEntity != null)
            {
                TestModel test = TestModel.Create(testEntity.Id, (CategoryModel.Create(testEntity.Category.Id, testEntity.Category.NameCategory).category), testEntity.NameTest).test;
                return test;
            }

            return null;
        }

        public async Task<Guid> Update(Guid id, CategoryModel? category, string? nameTest)
        {
            CategoryEntity categoryEntity = new CategoryEntity
            {
                Id = category.Id,
                NameCategory = category.NameCategory
            };

            await _context.Tests
                .Where(t => t.Id == id)
                .ExecuteUpdateAsync(t => t
                .SetProperty(t => t.Category, categoryEntity)
                .SetProperty(t => t.NameTest, nameTest));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Tests
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
