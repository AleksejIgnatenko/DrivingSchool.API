using DrivingSchool.Core.Models;

namespace DrivingSchool.DataAccess.Repositories
{
    public interface IQuestionRepository
    {
        Task<Guid> CreateAsync(QuestionModel question);
        Task<Guid> DeleteAsync(Guid id);
        Task<List<QuestionModel>> GetAsync();
        QuestionModel? GetByIdAsync(Guid id);
        Task<Guid> UpdateAsync(Guid id, Guid testId, string? questionText, string? linkPhoto, string? answer1, string? answer2, string? answer3, string? answer4, string? correctAnswer);
    }
}