using DrivingSchool.Core.Models;

namespace DrivingSchool.BusinessLogic.TestServices
{
    public interface ITestServices
    {
        Task<Guid> CreateTestAsync(TestModel test);
        Task<Guid> DeleteTestAsync(Guid id);
        Task<List<TestModel>> GetAllTestAsync();
        Task<TestModel> GetTestById(Guid id);
        Task<Guid> UpdateTestAsync(Guid id, Guid categoryId, string? nameTest);
    }
}