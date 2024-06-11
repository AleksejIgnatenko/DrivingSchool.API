using DrivingSchool.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.Core.Abstractions
{
    public interface IUsersRepository
    {
        Task<Guid> CreateUser(UserModel user);
        Task<Guid> DeleteUser(Guid idUser);
        Task<List<UserModel>> GetUsers();
        Task<Guid> UpdateUser(Guid idUser, string username, string email, string password, string role);
    }
}
