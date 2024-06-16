using DrivingSchool.Core.Models;

namespace DrivingSchool.BusinessLogic.CategoryServices
{
    public interface ICategoryServices
    {
        Task<Guid> CreateCategory(CategoryModel category);
        Task<Guid> DeleteCategory(Guid idCategory);
        Task<List<CategoryModel>> GetAllCategory();
        Task<Guid> UpdateCategory(Guid idCategory, string? nameCategory);
    }
}