using DrivingSchool.Core.Models;

namespace DrivingSchool.BusinessLogic.TestServices
{
    public interface ITestServices
    {
        Task<Guid> CreateTest(TestModel test);
        Task<Guid> DeleteTest(Guid id);
        Task<List<TestModel>> GetAllTest();
        Task<Guid> UpdateTest(Guid id, Guid categoryId, string? nameTest);
    }
}