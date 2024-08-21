using DrivingSchool.API.Contracts.QuestionContracts;
using DrivingSchool.API.Contracts.TestContracts;
using DrivingSchool.BusinessLogic.CategoryServices;
using DrivingSchool.BusinessLogic.TestServices;
using DrivingSchool.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.API.Controllers
{
    [ApiController]
    [Route("api/Test")]
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
        //[Authorize(Roles = "Admin,Moderator")]
        [Route("getAllTests")]
        public async Task<ActionResult<List<GetTestResponse>>> GetTestAsync()
        {
            try
            {
                var tests = await _testServices.GetAllTestAsync();

                var response = tests.Select(t => new GetTestResponse(t.Id, t.Category.NameCategory, t.NameTest));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        [Route("getCategoryTests")]
        public async Task<ActionResult<List<GetTestResponse>>> GetCategoryTestsAsync(Guid idCategory)
        {
            try
            {
                var tests = await _testServices.GetCategoryTestsAsync(idCategory);

                var response = tests.Select(t => new GetTestResponse(t.Id, t.Category.NameCategory, t.NameTest));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getCategoryTest")]
        public async Task<ActionResult<List<TestResponse>>> GetCategoryTest(Guid idCategory)
        {
            try
            {
                var test = await _categoryServices.GetCategoryTest(idCategory);

                var response = new TestCategoryResponse(test.Id, test.NameTest, test.Questions.Select(q => new QuestionResponse(q.Id,
                                                                                                                                test.NameTest,
                                                                                                                                q.QuestionText,
                                                                                                                                q.LinkPhoto,
                                                                                                                                q.Answer1,
                                                                                                                                q.Answer2,
                                                                                                                                q.Answer3,
                                                                                                                                q.Answer4,
                                                                                                                                q.CorrectAnswer)).ToList()
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
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
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult<GetTestResponse>> UpdateTestAsync(Guid id, [FromBody] TestRequest testRequest)
        {
            try
            {
                var test = await _testServices.UpdateTestAsync(id, testRequest.IdCategory, testRequest.NameTest);

                var response = new GetTestResponse(test.Id, test.Category.NameCategory, test.NameTest);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin,Moderator")]
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
