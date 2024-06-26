using DrivingSchool.Core.Models;

namespace DrivingSchool.DataAccess.Repositories
{
    public interface ICategoryRepository
    {
        Task<Guid> CreateAsync(CategoryModel category);
        Task<Guid> DeleteAsync(Guid id);
        Task<List<CategoryModel>> GetAsync();
        Task<CategoryModel> GetByIdAsync(Guid id);
        Task<Guid> UpdateAsync(Guid idCategory, string? nameCategory);
    }
}