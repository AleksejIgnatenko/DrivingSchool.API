using DrivingSchool.Core.Models;

namespace DrivingSchool.BusinessLogic.QuestionServices
{
    public interface IQuestionServices
    {
        Task<Guid> CreateQuestionAsync(QuestionModel question);
        Task<Guid> DeleteQuestionAsync(Guid id);
        Task<List<QuestionModel>> GetAllQuestionsAsync();
        Task<List<QuestionModel>> GetTestQuestionsAsync(Guid testId);
        Task<QuestionModel> UpdateQuestionAsync(Guid id, Guid testId, string? questionText, string? linkPhoto, string? answer1, string? answer2, string? answer3, string? answer4, string? correctAnswer);
    }
}