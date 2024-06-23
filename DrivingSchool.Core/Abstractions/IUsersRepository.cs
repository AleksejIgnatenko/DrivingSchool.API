using DrivingSchool.Core.Models;

namespace DrivingSchool.Core.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> Create(UserModel user);
        Task<Guid> Delete(Guid idUser);
        Task<List<UserModel>> Get();
        Task<Guid> Update(Guid idUser, string username, string email, string password, string role);
    }
}
