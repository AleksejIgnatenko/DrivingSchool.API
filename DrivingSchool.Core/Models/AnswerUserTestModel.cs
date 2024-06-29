
namespace DrivingSchool.Core.Models
{
    public class AnswerUserTestModel
    {
        public Guid Id { get; }
        public UserModel? User { get; }
        public TestModel? Test { get; }
        public int ResultTest { get; }

        private AnswerUserTestModel(Guid id, TestModel test, int resultTest)
        {
            Id = id;
            Test = test;
            ResultTest = resultTest;
        }

        private AnswerUserTestModel(Guid id, UserModel user, TestModel test, int resultTest)
        {
            Id = id;
            User = user;
            Test = test;
            ResultTest = resultTest;
        }

        public static (AnswerUserTestModel answer, string error) Create(Guid id, TestModel test, int resultTest)
        {
            string error = string.Empty;
            AnswerUserTestModel answerUserTestModel = new AnswerUserTestModel(id, test, resultTest);
            return (answerUserTestModel, error);
        }

        public static (AnswerUserTestModel answerUserTestModel, string error) Create(Guid id, UserModel user, TestModel test, int resultTest)
        {
            string error = string.Empty;
            AnswerUserTestModel answerUserTestModel = new AnswerUserTestModel(id, user, test, resultTest);
            return (answerUserTestModel, error);
        }
    }
}
