using DrivingSchool.API.Contracts.TestContracts;
using DrivingSchool.BusinessLogic.CategoryServices;
using DrivingSchool.BusinessLogic.TestServices;
using DrivingSchool.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestServices _testServices;
        private readonly ICategoryServices _categoryServices;

        public TestController(ITestServices testServices, ICategoryServices categoryServices)
        {
            this._testServices = testServices;
            this._categoryServices = categoryServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<TestResponse>>> GetTestAsync()
        {
            try
            {
                var tests = await _testServices.GetAllTestAsync();

                var response = tests.Select(t => new TestResponse(t.Id, t.Category.NameCategory, t.NameTest, t.Questions.Select(q => new QuestionModelView(q.Id, q.QuestionText, q.LinkPhoto, q.Answer1, q.Answer2, q.Answer3, q.Answer4, q.CorrectAnswer)).ToList()));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTestAsync([FromBody] TestRequest testRequest)
        {
            try
            {
                var (test, error) = TestModel.Create(
                    Guid.NewGuid(),
                    await _categoryServices.GetCategoryByIdAsync(testRequest.IdCategory),
                    testRequest.NameTest
                );

                if (!string.IsNullOrEmpty(error))
                {
                    return BadRequest(error);
                }

                var testId = await _testServices.CreateTestAsync(test);

                return Ok(testId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateTestAsync(Guid id, [FromBody] TestRequest testRequest)
        {
            try
            {
                var testId = await _testServices.UpdateTestAsync(id, testRequest.IdCategory, testRequest.NameTest);

                return Ok(testId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteTestAsync(Guid id)
        {
            try
            {
                return Ok(await _testServices.DeleteTestAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
