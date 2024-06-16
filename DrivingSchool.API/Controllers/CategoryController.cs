using DrivingSchool.API.Contracts.CategoryContracts;
using DrivingSchool.API.Contracts.UserContracts;
using DrivingSchool.BusinessLogic.CategoryServices;
using DrivingSchool.BusinessLogic.UserServices;
using DrivingSchool.Core.Abstractions;
using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            this._categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponse>>> GetCategory()
        {
            var category = await _categoryServices.GetAllCategory();

            var response = category.Select(c => new CategoryResponse(c.IdCategory, c.NameCategory));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCategory([FromBody] CategoryRequest categoryRequest)
        {
            var (user, error) = CategoryModel.Create(
                Guid.NewGuid(),
                categoryRequest.NameCategory
            );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var categoryId = await _categoryServices.CreateCategory(user);

            return Ok(categoryId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateCategory(Guid id, [FromBody] CategoryRequest categoryRequest)
        {
            var categoryId = await _categoryServices.UpdateCategory(id, categoryRequest.NameCategory);

            return Ok(categoryId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteCategory(Guid id)
        {
            return Ok(await _categoryServices.DeleteCategory(id));
        }
    }
}
