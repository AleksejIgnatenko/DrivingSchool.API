namespace DrivingSchool.DataAccess.Entities
{
    public class AnswerUserTestEntity
    {
        public Guid Id { get; set; }
        public UserEntity? User { get; set; }
        public TestEntity? Test { get; set; }
        public int ResultTest { get; set; }
    }
}
