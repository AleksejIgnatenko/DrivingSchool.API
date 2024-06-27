using DrivingSchool.Core.Models;
using DrivingSchool.MockData.Repositories;
using JetBrains.dotMemoryUnit;

namespace DrivingSchool.UnitTests.Repository
{
    public class QuestionRepositoryTest
    {
        private readonly QuestionRepositoryMock _repository;

        public QuestionRepositoryTest()
        {
            _repository = new QuestionRepositoryMock();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            TestModel test = TestModel.Create(Guid.NewGuid(), "Test").test;
            QuestionModel question = QuestionModel.Create(Guid.NewGuid(), test, "Text", "", "answer1", "answer2", "answer3", "answer4", "correctAnswer").question;

            // Act
            var id = await _repository.CreateAsync(question);

            // Assert
            Assert.Equal(question.Id, id);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task GetAsync()
        {
            // Arrange

            // Act
            var question = await _repository.GetAsync();

            // Assert
            Assert.Single(question);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            Guid id = Guid.Parse("64dc0c4f-74fc-483c-8fa3-7daa1b3264e7");
            // Act
            var question = await _repository.GetByIdAsync(id);

            // Assert
            Assert.Single(question);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            Guid questionId = Guid.Parse("d833d875-660f-4d6b-a138-796a6ae98095");
            Guid testId = Guid.Parse("5f51c061-b5f8-4579-928e-3858cd3013f5");
            string textQuestion = "update";
            string linkPhoto = "update";
            string answer1 = "update";
            string answer2 = "update";
            string answer3 = "update";
            string answer4 = "update";
            string correctAnswer = "update";

            // Act
            var id = await _repository.UpdateAsync(questionId, testId, textQuestion, linkPhoto, answer1, answer2, answer3, answer4, correctAnswer);

            // Assert
            Assert.Equal(questionId, id);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task DeleteAsync()
        {
            // Arrange
            Guid questionid = Guid.Parse("d833d875-660f-4d6b-a138-796a6ae98095");

            // Act
            var id = await _repository.DeleteAsync(questionid);

            // Assert
            Assert.Equal(questionid, id);
        }
    }
}
