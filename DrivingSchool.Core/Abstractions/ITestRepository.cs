using DrivingSchool.Core.Models;

namespace DrivingSchool.DataAccess.Repositories
{
    public interface ITestRepository
    {
        Task<Guid> CreateAsync(TestModel test);
        Task<Guid> DeleteAsync(Guid id);
        Task<List<TestModel>> GetAsync();
        Task<TestModel> GetByIdAsync(Guid id);
        Task<Guid> GetRandomCategoryTest(Guid idCategory);
        Task<List<TestModel>> GetCategoryTestsAsync(Guid id);
        Task<TestModel> UpdateAsync(Guid id, Guid categoryId, string? nameTest);
    }
}