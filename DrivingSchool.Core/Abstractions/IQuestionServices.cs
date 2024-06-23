using DrivingSchool.Core.Models;

namespace DrivingSchool.BusinessLogic.QuestionServices
{
    public interface IQuestionServices
    {
        Task<Guid> CreateQuestion(QuestionModel question);
        Task<Guid> DeleteQuestion(Guid id);
        Task<List<QuestionModel>> GetAllQuestions();
        Task<Guid> UpdateQuestion(Guid id, Guid testId, string? questionText, string? linkPhoto, string? answer1, string? answer2, string? answer3, string? answer4, string? correctAnswer);
    }
}