
using DrivingSchool.Core.Enum;

namespace DrivingSchool.Core.Models
{
    public class UserModel
    {
        public Guid Id { get; }
        public List<AnswerUserTestModel>? Answers { get; }
        public string? UserName { get; }
        public string? Email { get; }
        public string? Password { get; }
        public RoleEnum? Role { get; }

        private UserModel(Guid idUser, string? userName, string? email, string? password, RoleEnum? role)
        {
            Id = idUser;
            UserName = userName;
            Email = email;
            Password = password;
            Role = role;
        }

        private UserModel(Guid idUser, List<AnswerUserTestModel> answers, string? userName, string? email, string? password, RoleEnum? role) 
            : this(idUser, userName, email, password, role)
        {
            Answers = answers;
        }

        public static (UserModel user, string error) Create(Guid idUser, string? userName, string? email, string? password, RoleEnum? role)
        {
            string error = string.Empty;
            UserModel user = new UserModel(idUser, userName, email, password, role);
            return (user, error);
        }

        public static (UserModel user, string error) Create(Guid idUser, List<AnswerUserTestModel> answers, string? userName, string? email, string? password, RoleEnum? role)
        {
            string error = string.Empty;
            UserModel user = new UserModel(idUser, answers, userName, email, password, role);
            return (user, error);
        }
    }
}
