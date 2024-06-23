
using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.DataAccess.Entities
{
    public class QuestionEntity
    {
        public Guid Id { get; set; }
        public TestEntity? Test { get; set; }
        public string? QuestionText { get; set; }
        public string? LinkPhoto { get; set; }
        public string? Answer1 { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public string? CorrectAnswer { get; set; }
    }
}
