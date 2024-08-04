using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Repositories;

namespace DrivingSchool.BusinessLogic.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;

        public CategoryServices(ICategoryRepository categoryRepository,
            ITestRepository testRepository,
            IQuestionRepository questionRepository)
        {
            this._categoryRepository = categoryRepository;
            this._testRepository = testRepository;
            this._questionRepository = questionRepository;
        }

        public async Task<List<CategoryModel>> GetAllCategoryAsync()
        {
            return await _categoryRepository.GetAsync();
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(Guid idCategory)
        {
            return await _categoryRepository.GetByIdAsync(idCategory);
        }

        public async Task<List<QuestionModel>> GetCategoryTest(Guid idCategory)
        {
            var randomTestId = await _testRepository.GetRandomCategoryTest(idCategory);
            var questions = await _questionRepository.GetRandomTestQuestions(randomTestId);

            return questions;
        }

        public async Task<Guid> CreateCategoryAsync(CategoryModel category)
        {
            return await _categoryRepository.CreateAsync(category);
        }


        public async Task<CategoryModel> UpdateCategoryAsync(Guid idCategory, string? nameCategory)
        {
            return await _categoryRepository.UpdateAsync(idCategory, nameCategory);
        }

        public async Task<Guid> DeleteCategoryAsync(Guid idCategory)
        {
            return await _categoryRepository.DeleteAsync(idCategory);
        }
    }
}
