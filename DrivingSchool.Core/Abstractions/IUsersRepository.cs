using DrivingSchool.Core.Models;

namespace DrivingSchool.Core.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> CreateAsync(UserModel user);
        Task<Guid> DeleteAsync(Guid idUser);
        Task<List<UserModel>> GetAsync();
        Task<Guid> UpdateAsync(Guid idUser, string username, string email, string password, string role);
    }
}
