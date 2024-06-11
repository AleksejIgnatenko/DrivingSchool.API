using DrivingSchool.Core.Abstractions;
using DrivingSchool.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.BusinessLogic.UserServices
{
    public class UsersServices : IUsersServices
    {
        private readonly IUsersRepository _usersRepository;

        public UsersServices(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _usersRepository.GetUsers();
        }

        public async Task<Guid> CreateUser(UserModel user)
        {
            return await _usersRepository.CreateUser(user);
        }


        public async Task<Guid> UpdateUser(Guid idUser, string username, string email, string password, string role)
        {
            return await _usersRepository.UpdateUser(idUser, username, email, password, role);
        }

        public async Task<Guid> DeleteUser(Guid idUser)
        {
            return await _usersRepository.DeleteUser(idUser);
        }
    }
}
