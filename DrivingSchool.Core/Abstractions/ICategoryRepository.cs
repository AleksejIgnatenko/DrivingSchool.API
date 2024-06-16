using DrivingSchool.Core.Models;

namespace DrivingSchool.DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        Task<Guid> Create(CategoryModel category);
        Task<Guid> Delete(Guid id);
        Task<List<CategoryModel>> Get();
        Task<Guid> Update(Guid idCategory, string? nameCategory);
    }
}