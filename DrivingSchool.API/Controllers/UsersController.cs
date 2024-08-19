using DrivingSchool.API.Contracts.UserContracts;
using DrivingSchool.BusinessLogic.UserServices;
using DrivingSchool.Core.Enum;
using DrivingSchool.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.API.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersServices _usersServices;
        public UsersController(IUsersServices usersServices)
        {
            _usersServices = usersServices;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        [Route("getAllUsers")]
        public async Task<ActionResult<List<UsersResponse>>> GetUsersAsync()
        {
            try
            {
                var users = await _usersServices.GetAllUsersAsync();

                var response = users.Select(u => new UsersResponse(
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.Role.ToString(),
                    u.Answers
                        .GroupBy(a => a.Test.Category?.NameCategory) // Группировка по названию категории
                        .ToDictionary(g => g.Key, g => g.Select(a => a.ResultTest).ToArray())
                )).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("getUserInformationById")]
        public async Task<ActionResult<UsersResponse>> GetUserInformationById()
        {
            try
            {
                var user = await _usersServices.GetUsersByIdAsync(Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));

                var response = new UsersResponse(
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.Role.ToString(),
                    user.Answers
                        .GroupBy(a => a.Test.Category?.NameCategory) // Группировка по названию категории
                        .ToDictionary(g => g.Key, g => g.Select(a => a.ResultTest).ToArray())
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        [Route("isAdminOrModerator")]
        public ActionResult IsAdmin()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUserAsync([FromBody] UsersRequest usersRequest)
        {
            try
            {
                var (user, error) = UserModel.Create(
                    Guid.NewGuid(),
                    usersRequest.UserName,
                    usersRequest.Email,
                    usersRequest.Password,
                    RoleEnum.Admin
                    );

                if (!string.IsNullOrEmpty(error))
                {
                    return BadRequest(error);
                }

                var userId = await _usersServices.CreateUserAsync(user);

                return Ok(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<Guid>> RegisterUserAsync([FromBody] UsersRequest usersRequest)
        {
            try
            {
                var error = await _usersServices.RegisterUserAsync(usersRequest.UserName, usersRequest.Email, usersRequest.Password, RoleEnum.Admin);
                if (!string.IsNullOrEmpty(error))
                {
                    return BadRequest(error);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Guid>> LoginUserAsync([FromBody] LoginUserRequest loginUserRequest)
        {
            try
            {
                var (token, error) = await _usersServices.LoginUserAsync(loginUserRequest.Email, loginUserRequest.Password);
                if (!string.IsNullOrEmpty(error))
                {
                    return BadRequest(error);
                }

                //Response.Cookies.Append("test-cookies", token);

                return new JsonResult(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateUserAsync(Guid id, [FromBody] UpdateUsersRequest usersRequest)
        {
            try
            {
                var userId = await _usersServices.UpdateUserAsync(id, usersRequest.UserName, usersRequest.Email, usersRequest.Password, usersRequest.Role);

                return Ok(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("addModeratorRole/{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UsersResponse>> AddModerator(Guid id)
        {
            try
            {
                var user = await _usersServices.AddModeratorRole(id);

                var response = new UsersResponse(
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.Role.ToString(),
                    user.Answers
                        .GroupBy(a => a.Test.Category?.NameCategory) // Группировка по названию категории
                        .ToDictionary(g => g.Key, g => g.Select(a => a.ResultTest).ToArray())
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("deleteModeratorRole/{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UsersResponse>> DeleteModerator(Guid id)
        {
            try
            {
                var user = await _usersServices.DeleteModeratorRole(id);

                var response = new UsersResponse(
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.Role.ToString(),
                    user.Answers
                        .GroupBy(a => a.Test.Category?.NameCategory) // Группировка по названию категории
                        .ToDictionary(g => g.Key, g => g.Select(a => a.ResultTest).ToArray())
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("userNameChange/{id:guid}")]
        public async Task<ActionResult<UsersResponse>> UserNameChange(Guid id, [FromBody] string newUserName)
        {
            try
            {
                var user = await _usersServices.UserNameChange(id, newUserName);

                var response = new UsersResponse(
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.Role.ToString(),
                    user.Answers
                        .GroupBy(a => a.Test.Category?.NameCategory) // Группировка по названию категории
                        .ToDictionary(g => g.Key, g => g.Select(a => a.ResultTest).ToArray())
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteUserAsync(Guid id)
        {
            try
            {
                return Ok(await _usersServices.DeleteUserAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}