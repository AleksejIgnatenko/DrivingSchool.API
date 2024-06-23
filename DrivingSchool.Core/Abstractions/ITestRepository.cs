using DrivingSchool.Core.Models;

namespace DrivingSchool.DataAccess.Repositories
{
    public interface ITestRepository
    {
        Task<Guid> Create(TestModel test);
        Task<Guid> Delete(Guid id);
        Task<List<TestModel>> Get();
        TestModel? Get(Guid id);
        TestModel? GetById(Guid id);
        Task<Guid> Update(Guid id, Guid categoryId, string? nameTest);
    }
}