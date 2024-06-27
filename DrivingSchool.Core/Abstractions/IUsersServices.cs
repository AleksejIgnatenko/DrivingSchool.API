using DrivingSchool.Core.Models;

namespace DrivingSchool.BusinessLogic.UserServices
{
    public interface IUsersServices
    {
        Task<Guid> CreateUserAsync(UserModel user);
        Task<Guid> DeleteUserAsync(Guid idUser);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUsersByIdAsync(Guid idUser);
        Task<Guid> UpdateUserAsync(Guid idUser, string userName, string email, string password, string role);
    }
}