using DrivingSchool.Core.Models;

namespace DrivingSchool.DataAccess.Repositories
{
    public interface IAnswerUserTestRepository
    {
        Task<Guid> CreateAsync(AnswerUserTestModel answerUserTestModel);
        Task<Guid> DeleteAsync(Guid id);
        Task<List<AnswerUserTestModel>> GetAsync();
        Task<Guid> UpdateAsync(Guid id, Guid idUser, Guid idTest, int resultTest);
    }
}