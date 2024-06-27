using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;

namespace DrivingSchool.MockData.Repositories
{
    public class TestRepositoryMock
    {
        public void Dispose()
        {
        }

        public async Task<Guid> CreateAsync(TestModel test)
        {
            CategoryEntity categoryEntity = new CategoryEntity
            {
                Id = test.Category!.Id,
                NameCategory = test.Category.NameCategory
            };

            TestEntity testEntity = new TestEntity
            {
                Id = test.Id,
                Category = categoryEntity,
                NameTest = test.NameTest,
            };

            return await Task.FromResult(test.Id);
        }

        public async Task<List<TestModel>> GetAsync()
        {
            var categoryEntity = new CategoryEntity
            {
                Id = Guid.NewGuid(),
                NameCategory = "Category"
            };

            var questionEntities = new List<QuestionEntity>
            {
                new QuestionEntity{ 
                    Id = Guid.NewGuid(),
                    QuestionText = "Test",
                    LinkPhoto = "",
                    Answer1 = "Answer1",
                    Answer2 = "Answer2",
                    Answer3 = "Answer3",
                    Answer4 = "Answer4",
                    CorrectAnswer = "CorrectAnswer"
                }
            };

            var testEntities = new List<TestEntity>
            {
                new TestEntity {
                Id = Guid.NewGuid(),
                Category = categoryEntity,
                NameTest = "Test",
                Questions = questionEntities
                }
            };

            var tests = testEntities
                .Select(t => TestModel.Create(t.Id, (CategoryModel.Create(t.Category.Id, t.Category.NameCategory).category), t.NameTest, t.Questions.Select(q => QuestionModel.Create(q.Id, q.QuestionText, q.LinkPhoto, q.Answer1, q.Answer2, q.Answer3, q.Answer4, q.CorrectAnswer).question).ToList()).test)
                .ToList();

            return await Task.FromResult(tests);
        }

        public async Task<TestModel> GetByIdAsync(Guid id)
        {
            var categoryEntity = new CategoryEntity
            {
                Id = Guid.NewGuid(),
                NameCategory = "Category"
            };

            var questionEntities = new List<QuestionEntity>
            {
                new QuestionEntity{
                    Id = Guid.NewGuid(),
                    QuestionText = "Test",
                    LinkPhoto = "",
                    Answer1 = "Answer1",
                    Answer2 = "Answer2",
                    Answer3 = "Answer3",
                    Answer4 = "Answer4",
                    CorrectAnswer = "CorrectAnswer"
                }
            };

            var testEntities = new List<TestEntity>
            {
                new TestEntity {
                Id = Guid.Parse("5f51c061-b5f8-4579-928e-3858cd3013f5"),
                Category = categoryEntity,
                NameTest = "Test",
                Questions = questionEntities
                }
            };

            var testEntity = testEntities.FirstOrDefault(t => t.Id == id);

            if (testEntities != null)
            {
                TestModel test = TestModel.Create(testEntity.Id, (CategoryModel.Create(testEntity.Category.Id, testEntity.Category.NameCategory).category), testEntity.NameTest).test;
                return await Task.FromResult(test);
            }

            throw new Exception("Error search by id");
        }
    }
}
