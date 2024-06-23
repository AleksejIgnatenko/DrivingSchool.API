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
        public async Task<ActionResult<List<TestResponse>>> GetTest()
        {
            var tests = await _testServices.GetAllTest();

            var response = tests.Select(t => new TestResponse(t.Id, t.Category.NameCategory, t.NameTest, t.Questions.ToDictionary(q => q.Id, q => q.QuestionText)));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateTest([FromBody] TestRequest testRequest)
        {
            var (test, error) = TestModel.Create(
                Guid.NewGuid(),
                await _categoryServices.GetCategoryById(testRequest.IdCategory),
                testRequest.NameTest
            );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var testId = await _testServices.CreateTest(test);

            return Ok(testId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateTest(Guid id, [FromBody] TestRequest testRequest)
        {

            var testId = await _testServices.UpdateTest(id, testRequest.IdCategory, testRequest.NameTest);

            return Ok(testId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteTest(Guid id)
        {
            return Ok(await _testServices.DeleteTest(id));
        }
    }
}
