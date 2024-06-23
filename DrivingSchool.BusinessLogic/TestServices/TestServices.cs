using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Repositories;

namespace DrivingSchool.BusinessLogic.TestServices
{
    public class TestServices : ITestServices
    {
        private readonly ITestRepository _testRepository;

        public TestServices(ITestRepository testRepository)
        {
            this._testRepository = testRepository;
        }

        public async Task<List<TestModel>> GetAllTest()
        {
            return await _testRepository.Get();
        }

        public TestModel? GetTestById(Guid id)
        {
            return _testRepository.Get(id);
        }


        public async Task<Guid> CreateTest(TestModel test)
        {
            return await _testRepository.Create(test);
        }


        public async Task<Guid> UpdateTest(Guid id, Guid categoryId, string? nameTest)
        {
            return await _testRepository.Update(id, categoryId, nameTest);
        }

        public async Task<Guid> DeleteTest(Guid id)
        {
            return await _testRepository.Delete(id);
        }
    }
}
