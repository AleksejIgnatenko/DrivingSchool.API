using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.DataAccess.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DrivingSchoolDbContext _context;

        public QuestionRepository(DrivingSchoolDbContext context)
        {
            this._context = context;
        }

        public async Task<Guid> CreateAsync(QuestionModel question)
        {
            var existingTest = await _context.Tests
                .FirstOrDefaultAsync(t => t.Id == question.Test.Id);

            if (existingTest != null)
            {
                QuestionEntity questionEntity = new QuestionEntity
                {
                    Id = question.Id,
                    Test = existingTest,
                    QuestionText = question.QuestionText,
                    LinkPhoto = question.LinkPhoto,
                    Answer1 = question.Answer1,
                    Answer2 = question.Answer2,
                    Answer3 = question.Answer3,
                    Answer4 = question.Answer4,
                    CorrectAnswer = question.CorrectAnswer,
                };

                await _context.Questions.AddAsync(questionEntity);
                await _context.SaveChangesAsync();

                return question.Id;
            }

            throw new Exception("Ошибка добавления вопрсоа");
        }

        public async Task<List<QuestionModel>> GetAsync()
        {
            var questionEntity = await _context.Questions
                .AsNoTracking()
                .Include(t => t.Test)
                .ToListAsync();

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

            return question;
        }

        public QuestionModel? GetByIdAsync(Guid id)
        {
            var questionEntity = _context.Questions
                .FirstOrDefault(q => q.Id == id);

            if (questionEntity != null)
            {
                QuestionModel test = QuestionModel.Create(questionEntity.Id,
                                TestModel.Create(questionEntity.Test.Id, CategoryModel.Create(questionEntity.Test.Category.Id, questionEntity.Test.Category.NameCategory).category, questionEntity.QuestionText).test,
                                questionEntity.QuestionText,
                                questionEntity.LinkPhoto,
                                questionEntity.Answer1,
                                questionEntity.Answer2,
                                questionEntity.Answer3,
                                questionEntity.Answer4,
                                questionEntity.CorrectAnswer).question;
                return test;
            }

            return null;
        }

        public async Task<Guid> UpdateAsync(Guid id, Guid testId, string? questionText, string? linkPhoto, string? answer1, string? answer2, string? answer3, string? answer4, string? correctAnswer)
        {
            var questionEntity = await _context.Questions.FindAsync(id);
            var testEntity = await _context.Tests.FindAsync(testId);

            if ((questionEntity != null) && (testEntity != null))
            {
                questionEntity.Test = testEntity;
                questionEntity.QuestionText = questionText;
                questionEntity.LinkPhoto = linkPhoto;
                questionEntity.Answer1 = answer1;
                questionEntity.Answer2 = answer2;
                questionEntity.Answer3 = answer3;
                questionEntity.Answer4 = answer4;
                questionEntity.CorrectAnswer = correctAnswer;

                await _context.SaveChangesAsync();

                return id;
            }

            throw new Exception("Error for update test");
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            await _context.Questions
                .Where(q => q.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
