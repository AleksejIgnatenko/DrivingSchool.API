
namespace DrivingSchool.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public List<AnswerUserTestEntity>? AnswerUserTests { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
