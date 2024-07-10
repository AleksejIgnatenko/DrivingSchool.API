using DrivingSchool.Core.Models;

namespace DrivingSchool.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        Task<Guid> CreateAsync(UserModel user);
        Task<Guid> DeleteAsync(Guid idUser);
        Task<List<UserModel>> GetAsync();
        Task<UserModel> GetByEmailAsync(string email);
        Task<UserModel> GetByIdAsync(Guid id);
        Task<Guid> UpdateAsync(Guid idUser, string username, string email, string password, string role);
    }
}