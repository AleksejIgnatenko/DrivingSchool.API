using DrivingSchool.API.Contracts.UserContracts;
using DrivingSchool.BusinessLogic.UserServices;
using DrivingSchool.Core.Abstractions;
using DrivingSchool.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _usersServices;
        public UsersController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsersResponse>>> GetUsersAsync()
        {
            var users = await _usersServices.GetAllUsersAsync();

            var response = users.Select(u => new UsersResponse(u.Id, u.UserName, u.Email, u.Password, u.Role));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUserAsync([FromBody] UsersRequest usersRequest)
        {
            var (user, error) = UserModel.Create(
                Guid.NewGuid(),
                usersRequest.UserName,
                usersRequest.Email,
                usersRequest.Password,
                usersRequest.Role
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var userId = await _usersServices.CreateUserAsync(user);

            return Ok(userId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateUserAsync(Guid id, [FromBody] UsersRequest usersRequest)
        {
            var userId = await _usersServices.UpdateUserAsync(id, usersRequest.UserName, usersRequest.Email, usersRequest.Password, usersRequest.Role);

            return Ok(userId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteUserAsync(Guid id)
        {
            return Ok(await _usersServices.DeleteUserAsync(id));
        }
    }
}
