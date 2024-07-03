using DrivingSchool.Core.Models;
using DrivingSchool.MockData.Repositories;
using JetBrains.dotMemoryUnit;

namespace DrivingSchool.UnitTests.Repository
{
    public class AnswerUserTestRepositoryTest
    {
        private readonly AnswerUserTestRepositoryMock _repository;

        public AnswerUserTestRepositoryTest()
        {
            _repository = new AnswerUserTestRepositoryMock();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task CreateAsync()
        {
            UserModel user = UserModel.Create(Guid.NewGuid(), "userName", "email", "password", "admin").user;
            TestModel test = TestModel.Create(Guid.NewGuid(), "test").test;

            // Arrange
            AnswerUserTestModel answer = AnswerUserTestModel.Create(Guid.NewGuid(), user, test, 10).answer;

            // Act
            var id = await _repository.CreateAsync(answer);

            // Assert
            Assert.Equal(answer.Id, id);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task GetAsync()
        {
            // Arrange

            // Act
            var answers = await _repository.GetAsync();

            // Assert
            Assert.Single(answers);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            Guid answerId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            Guid testId = Guid.NewGuid();
            int resultTest = 0;

            // Act
            var id = await _repository.UpdateAsync(answerId, userId, testId, resultTest);
            // Assert
            Assert.Equal(answerId, id);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task DeleteAsync()
        {
            // Arrange
            Guid answerId = Guid.NewGuid();

            // Act
            var id = await _repository.DeleteAsync(answerId);

            // Assert
            Assert.Equal(answerId, id);
        }
    }
}
