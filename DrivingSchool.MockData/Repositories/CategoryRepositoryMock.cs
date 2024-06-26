using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;

namespace DrivingSchool.MockData.Repositories
{
    public class CategoryRepositoryMock
    {
        public void Dispose()
        {
        }

        public Task<List<CategoryModel>> Get()
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

            return Task.FromResult(categories);
        }
    }
}
