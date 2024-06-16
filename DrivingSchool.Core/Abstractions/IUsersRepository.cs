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
        Task<Guid> Create(UserModel user);
        Task<Guid> Delete(Guid idUser);
        Task<List<UserModel>> Get();
        Task<Guid> Update(Guid idUser, string username, string email, string password, string role);
    }
}
