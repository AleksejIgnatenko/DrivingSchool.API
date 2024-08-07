using DrivingSchool.API.Contracts.QuestionContracts;
using DrivingSchool.API.Contracts.TestContracts;
using DrivingSchool.BusinessLogic.QuestionServices;
using DrivingSchool.BusinessLogic.TestServices;
using DrivingSchool.Core.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Moderator")]
        [Route("getAllQuestions")]
        public async Task<ActionResult<List<QuestionResponse>>> GetQuestionAsync()
        {
            try
            {
                var question = await _questionServices.GetAllQuestionsAsync();

                var response = question.Select(q => new QuestionResponse(q.Id, q.Test.NameTest, q.QuestionText, q.LinkPhoto, q.Answer1, q.Answer2, q.Answer3, q.Answer4, q.CorrectAnswer));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        [Route("getTestQuestions")]
        public async Task<ActionResult<List<QuestionResponse>>> GetTestQuestions(Guid idTest)
        {
            try
            {
                var questions = await _questionServices.GetTestQuestionsAsync(idTest);

                var response = questions.Select(q => new QuestionResponse(q.Id,
                                                                          q.Test.NameTest,
                                                                          q.QuestionText,
                                                                          q.LinkPhoto,
                                                                          q.Answer1,
                                                                          q.Answer2,
                                                                          q.Answer3,
                                                                          q.Answer4,
                                                                          q.CorrectAnswer));

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult<Guid>> CreateQuestionAsync([FromBody] QuestionRequest questionRequest)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult<QuestionResponse>> UpdateQuestionAsync(Guid id, [FromBody] QuestionRequest questionRequest)
        {
            try
            {
                var question = await _questionServices.UpdateQuestionAsync(id, questionRequest.IdTest, questionRequest.QuestionText, questionRequest.LinkPhoto, questionRequest.Answer1, questionRequest.Answer2, questionRequest.Answer3, questionRequest.Answer4, questionRequest.CorrectAnswer);
                var response = new QuestionResponse(question.Id, question.Test.NameTest, question.QuestionText, question.LinkPhoto,
                    question.Answer1, question.Answer2, question.Answer3, question.Answer4, question.CorrectAnswer);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<ActionResult<Guid>> DeleteQuestionAsync(Guid id)
        {
            try
            {
                return Ok(await _questionServices.DeleteQuestionAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
