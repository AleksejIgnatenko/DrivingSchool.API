using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;

namespace DrivingSchool.MockData.Repositories
{
    public class QuestionRepositoryMock
    {
        public void Dispose()
        {
        }

        public async Task<Guid> CreateAsync(QuestionModel question, CancellationToken ct = default)
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

            return await questionEntity.Id.AsTask();
        }

        public async Task<List<QuestionModel>> GetAsync(CancellationToken ct = default)
        {
            var testEntity = new TestEntity
            {
                Id = Guid.NewGuid(),
                NameTest = "Test",
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

            var questions = questionEntity
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

            return await questions.AsTask();
        }

        public async Task<QuestionModel> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var testEntity = new TestEntity
            {
                Id = Guid.NewGuid(),
                NameTest = "Test category",
            };

            var questionEntities = new List<QuestionEntity>
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

            questionEntities[0].Test = testEntity;

            var questionEntity = questionEntities.FirstOrDefault(q => q.Id == id);

            var question = QuestionModel.Create(questionEntity.Id,
                                TestModel.Create(questionEntity.Test.Id, questionEntity.Test.NameTest).test,
                                questionEntity.QuestionText,
                                questionEntity.LinkPhoto,
                                questionEntity.Answer1,
                                questionEntity.Answer2,
                                questionEntity.Answer3,
                                questionEntity.Answer4,
                                questionEntity.CorrectAnswer).question;

            return await question.AsTask();
        }

        public async Task<Guid> UpdateAsync(Guid idQuestion, Guid idTest, string questionText, string linkPhoto, string answer1, string answer2, string answer3, string answer4, string correctAnswer)
        {
            return await idQuestion.AsTask();
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            return await id.AsTask();
        }
    }
}
