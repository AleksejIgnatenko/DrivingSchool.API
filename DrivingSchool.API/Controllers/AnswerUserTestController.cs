﻿using DrivingSchool.API.Contracts.AnswerUserTestContracts;
using DrivingSchool.BusinessLogic.AnswerUserTestServices;
using DrivingSchool.BusinessLogic.TestServices;
using DrivingSchool.BusinessLogic.UserServices;
using DrivingSchool.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerUserTestController : ControllerBase
    {
        private readonly IAnswerUserTestServices _answerUserTestServices;
        private readonly IUsersServices _usersServices;
        private readonly ITestServices _testServices;

        public AnswerUserTestController(IAnswerUserTestServices answerUserTestServices, IUsersServices usersServices, ITestServices testServices)
        {
            this._answerUserTestServices = answerUserTestServices;
            this._usersServices = usersServices;
            this._testServices = testServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<AnswerUserTestResponse>>> GetAnswerUserTestAsync()
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

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateAnswerUserTestAsync([FromBody] AnswerUserTestRequest answerUserTestRequest)
        {
            var (answer, error) = AnswerUserTestModel.Create(
                Guid.NewGuid(),
                await _usersServices.GetUsersByIdAsync(answerUserTestRequest.UserId),
                await _testServices.GetTestById(answerUserTestRequest.TestId),
                answerUserTestRequest.ResultTest
            );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var answerUserTestId = await _answerUserTestServices.CreateAnswerUserTestAsync(answer);

            return Ok(answerUserTestId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateAnswerUserTestAsync(Guid id, [FromBody] AnswerUserTestRequest answerUserTestRequest)
        {

            var answerUserTestId = await _answerUserTestServices.UpdateAnswerUserTestAsync(id, answerUserTestRequest.UserId, answerUserTestRequest.TestId, answerUserTestRequest.ResultTest);

            return Ok(answerUserTestId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteQuestionAsync(Guid id)
        {
            return Ok(await _answerUserTestServices.DeleteAnswerUserAsync(id));
        }
    }
}
