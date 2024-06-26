using DrivingSchool.API.Contracts.CategoryContracts;
using DrivingSchool.API.Contracts.QuestionContracts;
using DrivingSchool.API.Contracts.TestContracts;
using DrivingSchool.BusinessLogic.QuestionServices;
using DrivingSchool.BusinessLogic.TestServices;
using DrivingSchool.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionServices _questionServices;
        private readonly ITestServices _testServices;

        public QuestionController(IQuestionServices questionServices, ITestServices testServices)
        {
            this._questionServices = questionServices;
            this._testServices = testServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<QuestionResponse>>> GetQuestionAsync()
        {
            var question = await _questionServices.GetAllQuestionsAsync();

            var response = question.Select(q => new QuestionResponse(q.Id, q.Test.NameTest, q.QuestionText, q.LinkPhoto, q.Answer1, q.Answer2, q.Answer3, q.Answer4, q.CorrectAnswer));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateQuestionAsync([FromBody] QuestionRequest questionRequest)
        {
            var (question, error) = QuestionModel.Create(
                Guid.NewGuid(),
                await _testServices.GetTestById(questionRequest.IdTest),
                questionRequest.QuestionText,
                questionRequest.LinkPhoto,
                questionRequest.Answer1,
                questionRequest.Answer2,
                questionRequest.Answer3,
                questionRequest.Answer4,
                questionRequest.CorrectAnswer
            );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var questionId = await _questionServices.CreateQuestionAsync(question);

            return Ok(questionId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateQuestionAsync(Guid id, [FromBody] QuestionRequest questionRequest)
        {

            var questionId = await _questionServices.UpdateQuestionAsync(id, questionRequest.IdTest, questionRequest.QuestionText, questionRequest.LinkPhoto, questionRequest.Answer1, questionRequest.Answer2, questionRequest.Answer3, questionRequest.Answer4, questionRequest.CorrectAnswer);

            return Ok(questionId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteQuestionAsync(Guid id)
        {
            return Ok(await _questionServices.DeleteQuestionAsync(id));
        }
    }
}
