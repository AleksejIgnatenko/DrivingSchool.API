using DrivingSchool.Core.Models;

namespace DrivingSchool.BusinessLogic.CategoryServices
{
    public interface ICategoryServices
    {
        Task<Guid> CreateCategoryAsync(CategoryModel category);
        Task<Guid> DeleteCategoryAsync(Guid idCategory);
        Task<List<CategoryModel>> GetAllCategoryAsync();
        Task<CategoryModel> GetCategoryByIdAsync(Guid idCategory);
        Task<TestModel> GetCategoryTest(Guid idCategory);
        Task<CategoryModel> UpdateCategoryAsync(Guid idCategory, string? nameCategory);
    }
}