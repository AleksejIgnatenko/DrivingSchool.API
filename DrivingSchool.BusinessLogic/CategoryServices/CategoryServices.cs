using DrivingSchool.Core.Abstractions;
using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.BusinessLogic.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryModel>> GetAllCategory()
        {
            return await _categoryRepository.Get();
        }

        public async Task<CategoryModel> GetCategoryById(Guid idCategory)
        {
            return await _categoryRepository.Get(idCategory);
        }

        public async Task<Guid> CreateCategory(CategoryModel category)
        {
            return await _categoryRepository.Create(category);
        }


        public async Task<Guid> UpdateCategory(Guid idCategory, string? nameCategory)
        {
            return await _categoryRepository.Update(idCategory, nameCategory);
        }

        public async Task<Guid> DeleteCategory(Guid idCategory)
        {
            return await _categoryRepository.Delete(idCategory);
        }
    }
}
