﻿using DrivingSchool.API.Contracts.QuestionContracts;
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

        [HttpPost]
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
        public async Task<ActionResult<Guid>> UpdateQuestionAsync(Guid id, [FromBody] QuestionRequest questionRequest)
        {
            try
            {
                var questionId = await _questionServices.UpdateQuestionAsync(id, questionRequest.IdTest, questionRequest.QuestionText, questionRequest.LinkPhoto, questionRequest.Answer1, questionRequest.Answer2, questionRequest.Answer3, questionRequest.Answer4, questionRequest.CorrectAnswer);

                return Ok(questionId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
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
