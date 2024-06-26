using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;

namespace DrivingSchool.MockData.Repositories
{
    public class QuestionRepositoryMock
    {
        public void Dispose()
        {
        }

        public async Task<Guid> CreateAsync(QuestionModel question)
        {
            TestEntity testEntity = new TestEntity
            {
                Id = question.Test!.Id,
                NameTest = question.Test.NameTest,
            };

            QuestionEntity questionEntity = new QuestionEntity
            {
                Id = question.Id,
                QuestionText = question.QuestionText,
                LinkPhoto = question.LinkPhoto,
                Answer1 = question.Answer1,
                Answer2 = question.Answer2,
                Answer3 = question.Answer3,
                Answer4 = question.Answer4,
                CorrectAnswer = question.CorrectAnswer,
            };

            return await Task.FromResult(questionEntity.Id);
        }

        public async Task<List<QuestionModel>> GetAsync()
        {
            var testEntity = new TestEntity
            {
                Id = Guid.NewGuid(),
                NameTest = "Test category",
            };

            var questionEntity = new List<QuestionEntity>
            {
                new QuestionEntity
                {
                    Id = Guid.NewGuid(),
                    Test = testEntity,
                    QuestionText = "Test",
                    LinkPhoto = "",
                    Answer1 = "Answer1",
                    Answer2 = "Answer2",
                    Answer3 = "Answer3",
                    Answer4 = "Answer4",
                    CorrectAnswer = "CorrectAnswer"
                }
            };

            questionEntity[0].Test = testEntity;

            var question = questionEntity
                .Select(q => QuestionModel.Create(q.Id,
                                TestModel.Create(q.Test.Id, q.Test.NameTest).test,
                                q.QuestionText,
                                q.LinkPhoto,
                                q.Answer1,
                                q.Answer2,
                                q.Answer3,
                                q.Answer4,
                                q.CorrectAnswer).question)
                .ToList();

            return await Task.FromResult(question);
        }

        public async Task<List<QuestionModel>> GetByIdAsync(Guid id)
        {
            var testEntity = new TestEntity
            {
                Id = Guid.NewGuid(),
                NameTest = "Test category",
            };

            var questionEntity = new List<QuestionEntity>
            {
                new QuestionEntity
                {
                    Id = Guid.Parse("64dc0c4f-74fc-483c-8fa3-7daa1b3264e7"),
                    Test = testEntity,
                    QuestionText = "Test",
                    LinkPhoto = "",
                    Answer1 = "Answer1",
                    Answer2 = "Answer2",
                    Answer3 = "Answer3",
                    Answer4 = "Answer4",
                    CorrectAnswer = "CorrectAnswer"
                }
            };

            questionEntity[0].Test = testEntity;

            var question = questionEntity
                .Select(q => QuestionModel.Create(q.Id,
                                TestModel.Create(q.Test.Id, q.Test.NameTest).test,
                                q.QuestionText,
                                q.LinkPhoto,
                                q.Answer1,
                                q.Answer2,
                                q.Answer3,
                                q.Answer4,
                                q.CorrectAnswer).question)
                .ToList();

            return await Task.FromResult(question);
        }

        public async Task<Guid> UpdateAsync(Guid idQuestion, Guid idTest, string questionText, string linkPhoto, string answer1, string answer2, string answer3, string answer4, string correctAnswer)
        {
            return await Task.FromResult(idQuestion);
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            return await Task.FromResult(id);
        }
    }
}
