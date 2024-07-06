using DrivingSchool.API.Contracts.CategoryContracts;
using DrivingSchool.BusinessLogic.CategoryServices;
using DrivingSchool.Core.Models;
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
        public async Task<ActionResult<List<CategoryResponse>>> GetCategoryAsync()
        {
            try
            {
                var category = await _categoryServices.GetAllCategoryAsync();

                var response = category.Select(c => new CategoryResponse(c.Id, c.NameCategory, c.Tests.ToDictionary(t => t.Id, t => t.NameTest)));

                return Ok(response);
            }
            catch (Exception ex) 
            { 
                return StatusCode(500, ex); 
            }
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCategoryAsync([FromBody] CategoryRequest categoryRequest)
        {
            try
            {
                var (category, error) = CategoryModel.Create(
                    Guid.NewGuid(),
                    categoryRequest.NameCategory
                );

                if (!string.IsNullOrEmpty(error))
                {
                    return BadRequest(error);
                }

                var categoryId = await _categoryServices.CreateCategoryAsync(category);

                return Ok(categoryId);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateCategoryAsync(Guid id, [FromBody] CategoryRequest categoryRequest)
        {
            try
            {
                var categoryId = await _categoryServices.UpdateCategoryAsync(id, categoryRequest.NameCategory);

                return Ok(categoryId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteCategoryAsync(Guid id)
        {
            try
            {
                return Ok(await _categoryServices.DeleteCategoryAsync(id));
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex);
            }
        }
    }
}
