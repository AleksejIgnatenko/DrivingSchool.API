using DrivingSchool.Core.Models;

namespace DrivingSchool.BusinessLogic.AnswerUserTestServices
{
    public interface IAnswerUserTestServices
    {
        Task<Guid> CreateAnswerUserTestAsync(AnswerUserTestModel answerUserTestModel);
        Task<Guid> DeleteCategoryAsync(Guid idAnswerUserTest);
        Task<List<AnswerUserTestModel>> GetAllAnswerUserTestAsync();
        Task<Guid> UpdateAnswerUserTestAsync(Guid id, Guid idUser, Guid idTest, int resultTest);
    }
}