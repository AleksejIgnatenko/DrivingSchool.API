using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.Core.Models
{
    public class UserModel
    {
        public Guid IdUser { get; }
        public string UserName { get; } = string.Empty;
        public string Email { get; } = string.Empty;
        public string Password { get; } = string.Empty;
        public string Role { get; } = string.Empty;

        private UserModel(Guid idUser, string userName, string email, string password, string role)
        {
            IdUser = idUser;
            UserName = userName;
            Email = email;
            Password = password;
            Role = role;
        }

        public static (UserModel user, string error) Create(Guid idUser, string userName, string email, string password, string role)
        {
            string error = string.Empty;
            UserModel user = new UserModel(idUser, userName, email, password, role);
            return (user, error);
        }
    }
}
