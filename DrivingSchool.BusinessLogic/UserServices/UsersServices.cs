using DrivingSchool.Core.Abstractions;
using DrivingSchool.Core.Models;

namespace DrivingSchool.BusinessLogic.UserServices
{
    public class UsersServices : IUsersServices
    {
        private readonly IUsersRepository _usersRepository;

        public UsersServices(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            return await _usersRepository.GetAsync();
        }

        public async Task<Guid> CreateUserAsync(UserModel user)
        {
            return await _usersRepository.CreateAsync(user);
        }


        public async Task<Guid> UpdateUserAsync(Guid idUser, string userName, string email, string password, string role)
        {
            return await _usersRepository.UpdateAsync(idUser, userName, email, password, role);
        }

        public async Task<Guid> DeleteUserAsync(Guid idUser)
        {
            return await _usersRepository.DeleteAsync(idUser);
        }
    }
}
