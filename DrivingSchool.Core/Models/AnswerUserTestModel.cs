
using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Core.Models
{
    public class AnswerUserTestModel
    {
        public Guid Id { get; }
        public UserModel? User { get; }
        public TestModel? Test { get; }
        [Range(0, 10, ErrorMessage = "Результат должен быть в диапазоне от 0 до 10.")]
        public int ResultTest { get; }

        private AnswerUserTestModel(Guid id, TestModel test, int resultTest)
        {
            Id = id;
            Test = test;
            ResultTest = resultTest;
        }

        private AnswerUserTestModel(Guid id, UserModel user, TestModel test, int resultTest) : this(id, test, resultTest)
        {
            User = user;
        }

        public static (AnswerUserTestModel answer, string error) Create(Guid id, TestModel test, int resultTest)
        {
            AnswerUserTestModel answerUserTestModel = new AnswerUserTestModel(id, test, resultTest);
            string error = Validation(answerUserTestModel);
            return (answerUserTestModel, error);
        }

        public static (AnswerUserTestModel answer, string error) Create(Guid id, UserModel user, TestModel test, int resultTest)
        {
            AnswerUserTestModel answerUserTestModel = new AnswerUserTestModel(id, user, test, resultTest);
            string error = Validation(answerUserTestModel);
            return (answerUserTestModel, error);
        }

        private static string Validation(AnswerUserTestModel answerUserTestModel)
        {
            var context = new ValidationContext(answerUserTestModel);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(answerUserTestModel, context, results, true))
            {
                string errors = string.Empty;
                foreach (var error in results)
                {
                    errors += error + "\n";
                }
                return errors;
            }
            return string.Empty;
        }
    }
}
