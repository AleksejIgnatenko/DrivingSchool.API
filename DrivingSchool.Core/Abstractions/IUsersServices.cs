﻿using DrivingSchool.Core.Enum;
using DrivingSchool.Core.Models;

namespace DrivingSchool.BusinessLogic.UserServices
{
    public interface IUsersServices
    {
        Task<Guid> CreateUserAsync(UserModel user);
        Task<Guid> DeleteUserAsync(Guid idUser);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUsersByIdAsync(Guid idUser);
        Task<UserModel> GetUsersByIdAsync(string jwtToken);
        Task<(string token, string error)> LoginUserAsync(string email, string password);
        Task<string> RegisterUserAsync(string userName, string email, string password, RoleEnum role);
        Task<Guid> UpdateUserAsync(Guid idUser, string userName, string email, string password, RoleEnum role);
        Task<UserModel> AddModeratorRole(Guid idUser);
        Task<UserModel> DeleteModeratorRole(Guid idUser);
        Task<UserModel> UserNameChange(Guid idUser, string newUserName);
    }
}