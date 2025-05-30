﻿using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;
using DrivingSchool.Infrastructure.CustomException;
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

            if (existingCategory != null)
            {

                TestEntity testEntity = new TestEntity
                {
                    Id = test.Id,
                    Category = existingCategory,
                    NameTest = test.NameTest
                };

                await _context.Tests.AddAsync(testEntity);
                await _context.SaveChangesAsync();

                return testEntity.Id;
            }

            throw new CustomException("Не удалось создать категорию");
        }

        public async Task<List<TestModel>> GetAsync()
        {
            var testEntity = await _context.Tests
                .AsNoTracking()
                .Include(t => t.Category)
                .Include(t => t.Questions)
                .ToListAsync();

            var tests = testEntity
                .Select(t => TestModel.Create(t.Id,
                                (CategoryModel.Create(t.Category.Id, t.Category.NameCategory).category),
                        t.Questions.Select(q => QuestionModel.Create(q.Id,
                                                                     q.QuestionText,
                                                                     q.LinkPhoto,
                                                                     q.Answer1,
                                                                     q.Answer2,
                                                                     q.Answer3,
                                                                     q.Answer4,
                                                                     q.CorrectAnswer).question).ToList(),
                        t.NameTest).test)
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

            throw new CustomException("Не удалось  найти запрашиваемый тест");
        }

        public async Task<List<TestModel>> GetCategoryTestsAsync(Guid idCategory)
        {
            var testsEntities = await _context.Tests
                .AsNoTracking()
                .Include(t => t.Category)
                .Where(t => t.Category.Id == idCategory)
                .ToListAsync();

            if (testsEntities != null)
            {
                var tests = testsEntities
                    .Select(t => TestModel.Create(t.Id, CategoryModel.Create(t.Category.Id, t.Category.NameCategory).category, t.NameTest).test)
                    .ToList();

                return tests;
            }

            throw new CustomException("Не удалось получить тесты категории");
        }

        public async Task<TestModel> GetRandomCategoryTest(Guid idCategory)
        {
            var randomTest = await _context.Tests
                .AsNoTracking()
                .Include(t => t.Questions)
                .Where(t => t.Category.Id == idCategory && t.Questions.Count >= 10)
                .OrderBy(x => Guid.NewGuid())
                .FirstOrDefaultAsync();

            if (randomTest != null)
            {
                randomTest.Questions = randomTest.Questions?.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

                var test = TestModel.Create(randomTest.Id,
                                                randomTest.Questions.Select(q => QuestionModel.Create(q.Id,
                                                                                                      q.QuestionText,
                                                                                                      q.LinkPhoto,
                                                                                                      q.Answer1,
                                                                                                      q.Answer2,
                                                                                                      q.Answer3,
                                                                                                      q.Answer4,
                                                                                                      q.CorrectAnswer).question).ToList(),
                                            randomTest.NameTest).test;

                return test;
            }

            throw new Exception("Не удалось получить тест категории");
        }

        public async Task<TestModel> UpdateAsync(Guid id, Guid categoryId, string? nameTest)
        {
            var testEntity = await _context.Tests.FindAsync(id);
            var categoryEntity = await _context.Categories.FindAsync(categoryId);

            if ((testEntity != null) && (categoryEntity != null))
            {
                testEntity.Category = categoryEntity;
                testEntity.NameTest = nameTest;

                await _context.SaveChangesAsync();

                var test = TestModel.Create(
                    testEntity.Id,
                        CategoryModel.Create(testEntity.Category.Id, testEntity.Category.NameCategory).category,
                    testEntity.NameTest).test;

                return test;
            }

            throw new CustomException("Не удалось обновить тест");
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