using DrivingSchool.Core.Models;

namespace DrivingSchool.DataAccess.Repositories
{
    public interface IQuestionRepository
    {
        Task<Guid> Create(QuestionModel question);
        Task<Guid> Delete(Guid id);
        Task<List<QuestionModel>> Get();
        QuestionModel? GetById(Guid id);
        Task<Guid> Update(Guid id, Guid testId, string? questionText, string? linkPhoto, string? answer1, string? answer2, string? answer3, string? answer4, string? correctAnswer);
    }
}