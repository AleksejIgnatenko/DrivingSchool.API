using DrivingSchool.API.Contracts.AnswerUserTestContracts;
using DrivingSchool.API.Contracts.CategoryContracts;
using DrivingSchool.BusinessLogic.AnswerUserTestServices;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerUserTestController : ControllerBase
    {
        private readonly IAnswerUserTestServices _answerUserTestServices;

        public AnswerUserTestController(IAnswerUserTestServices answerUserTestServices)
        {
            this._answerUserTestServices = answerUserTestServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<AnswerUserTestResponse>>> GetCategoryAsync()
        {
            try
            {
                var answer = await _answerUserTestServices.GetAllAnswerUserTestAsync();

                var response = answer.Select(a => new AnswerUserTestResponse(a.Id, a.User.UserName, a.Test.NameTest, a.ResultTest));

                return Ok(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }
    }
}
