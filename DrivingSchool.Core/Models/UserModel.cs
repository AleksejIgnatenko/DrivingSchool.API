
namespace DrivingSchool.Core.Models
{
    public class UserModel
    {
        public Guid Id { get; }
        public List<AnswerUserTestModel>? Answers { get; }
        public string? UserName { get; }
        public string? Email { get; }
        public string? Password { get; }
        public string? Role { get; }

        private UserModel(Guid idUser, string? userName, string? email, string? password, string? role)
        {
            Id = idUser;
            UserName = userName;
            Email = email;
            Password = password;
            Role = role;
        }

        private UserModel(Guid idUser, List<AnswerUserTestModel> answers, string? userName, string? email, string? password, string? role)
        {
            Id = idUser;
            Answers = answers;
            UserName = userName;
            Email = email;
            Password = password;
            Role = role;
        }

        public static (UserModel user, string error) Create(Guid idUser, string? userName, string? email, string? password, string? role)
        {
            string error = string.Empty;
            UserModel user = new UserModel(idUser, userName, email, password, role);
            return (user, error);
        }

        public static (UserModel user, string error) Create(Guid idUser, List<AnswerUserTestModel> answers, string? userName, string? email, string? password, string? role)
        {
            string error = string.Empty;
            UserModel user = new UserModel(idUser, answers, userName, email, password, role);
            return (user, error);
        }
    }
}
