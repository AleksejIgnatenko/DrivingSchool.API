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

        public async Task<List<TestModel>> GetAllTestAsync()
        {
            return await _testRepository.GetAsync();
        }

        public async Task<List<TestModel>> GetCategoryTestsAsync(Guid idCategory)
        {
            return await _testRepository.GetCategoryTestsAsync(idCategory);
        }

        public async Task<TestModel> GetTestById(Guid id)
        {
            return await _testRepository.GetByIdAsync(id);
        }


        public async Task<Guid> CreateTestAsync(TestModel test)
        {
            return await _testRepository.CreateAsync(test);
        }


        public async Task<Guid> UpdateTestAsync(Guid id, Guid categoryId, string? nameTest)
        {
            return await _testRepository.UpdateAsync(id, categoryId, nameTest);
        }

        public async Task<Guid> DeleteTestAsync(Guid id)
        {
            return await _testRepository.DeleteAsync(id);
        }
    }
}
