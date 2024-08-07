using DrivingSchool.Core.Enum;
using DrivingSchool.Core.Models;

namespace DrivingSchool.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task<Guid> CreateAsync(UserModel user);
        Task<Guid> DeleteAsync(Guid idUser);
        Task<List<UserModel>> GetUserAsync();
        Task<UserModel> GetByEmailAsync(string email);
        Task<UserModel> GetUserByIdAsync(Guid id);
        Task<Guid> UpdateAsync(Guid idUser, string username, string email, string password, RoleEnum role);
        Task<UserModel> AddModerator(Guid idUser);
        Task<UserModel> DeleteModerator(Guid idUser);
        Task<UserModel> UserNameChange(Guid idUser, string newUserName);
    }
}