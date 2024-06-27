

using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Repositories;

namespace DrivingSchool.BusinessLogic.AnswerUserTestServices
{
    public class AnswerUserTestServices : IAnswerUserTestServices
    {
        private readonly IAnswerUserTestRepository _answerUserTestRepository;

        public AnswerUserTestServices(IAnswerUserTestRepository answerUserTestRepository)
        {
            this._answerUserTestRepository = answerUserTestRepository;
        }

        public async Task<Guid> CreateAnswerUserTestAsync(AnswerUserTestModel answerUserTestModel)
        {
            return await _answerUserTestRepository.CreateAsync(answerUserTestModel);
        }

        public async Task<List<AnswerUserTestModel>> GetAllAnswerUserTestAsync()
        {
            return await _answerUserTestRepository.GetAsync();
        }


        public async Task<Guid> UpdateAnswerUserTestAsync(Guid id, Guid idUser, Guid idTest, int resultTest)
        {
            return await _answerUserTestRepository.UpdateAsync(id, idUser, idTest, resultTest);
        }

        public async Task<Guid> DeleteAnswerUserAsync(Guid idAnswerUserTest)
        {
            return await _answerUserTestRepository.DeleteAsync(idAnswerUserTest);
        }
    }
}
