using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.DataAccess.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly DrivingSchoolDbContext _context;

        public TestRepository(DrivingSchoolDbContext context)
        {
            this._context = context;
        }

        public async Task<Guid> CreateAsync(TestModel test)
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

        public async Task<List<TestModel>> GetAsync()
        {
            var testEntity = await _context.Tests
                .AsNoTracking()
                .Include(t => t.Category)
                .Include(t => t.Questions)
                .ToListAsync();

            var tests = testEntity
                .Select(t => TestModel.Create(t.Id, (CategoryModel.Create(t.Category.Id, t.Category.NameCategory).category), t.NameTest, t.Questions.Select(q => QuestionModel.Create(q.Id, q.QuestionText, q.LinkPhoto, q.Answer1, q.Answer2, q.Answer3, q.Answer4, q.CorrectAnswer).question).ToList()).test)
                .ToList();

            return tests;
        }

        public async Task<TestModel> GetByIdAsync(Guid id)
        {
            var testEntity = await _context.Tests
                .FirstOrDefaultAsync(t => t.Id == id);

            if (testEntity != null)
            {
                TestModel test = TestModel.Create(testEntity.Id, testEntity.NameTest).test;
                return test;
            }

            throw new Exception("Error search by id");
        }

        public async Task<Guid> UpdateAsync(Guid id, Guid categoryId, string? nameTest)
        {
            var testEntity = await _context.Tests.FindAsync(id);
            var categoryEntity = await _context.Categories.FindAsync(categoryId);

            if ((testEntity != null) && (categoryEntity != null))
            {
                testEntity.Category = categoryEntity;
                testEntity.NameTest = nameTest;

                await _context.SaveChangesAsync();

                return id;
            }

            throw new Exception("Error for update test");
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            await _context.Tests
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
