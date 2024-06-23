using DrivingSchool.Core.Models;

namespace DrivingSchool.Core.Abstractions
{
    public interface IUsersServices
    {
        Task<Guid> CreateUser(UserModel user);
        Task<Guid> DeleteUser(Guid idUser);
        Task<List<UserModel>> GetAllUsers();
        Task<Guid> UpdateUser(Guid idUser, string username, string email, string password, string role);
    }
}
