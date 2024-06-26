using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Repositories;

namespace DrivingSchool.BusinessLogic.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryModel>> GetAllCategoryAsync()
        {
            return await _categoryRepository.GetAsync();
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(Guid idCategory)
        {
            return await _categoryRepository.GetByIdAsync(idCategory);
        }

        public async Task<Guid> CreateCategoryAsync(CategoryModel category)
        {
            return await _categoryRepository.CreateAsync(category);
        }


        public async Task<Guid> UpdateCategoryAsync(Guid idCategory, string? nameCategory)
        {
            return await _categoryRepository.UpdateAsync(idCategory, nameCategory);
        }

        public async Task<Guid> DeleteCategoryAsync(Guid idCategory)
        {
            return await _categoryRepository.DeleteAsync(idCategory);
        }
    }
}
