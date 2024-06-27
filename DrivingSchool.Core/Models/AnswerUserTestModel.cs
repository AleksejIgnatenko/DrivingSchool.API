using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingSchool.Core.Models
{
    public class AnswerUserTestModel
    {
        public Guid Id { get; set; }
        public UserModel? User { get; set; }
        public TestModel? Test { get; set; }
        public int ResultTest { get; set; }

        private AnswerUserTestModel(Guid id, UserModel user, TestModel test, int resultTest)
        {
            Id = id;
            User = user;
            Test = test;
            ResultTest = resultTest;
        }

        public static (AnswerUserTestModel answerUserTestModel, string error) Create(Guid id, UserModel user, TestModel test, int resultTest)
        {
            string error = string.Empty;
            AnswerUserTestModel answerUserTestModel = new AnswerUserTestModel(id, user, test, resultTest);
            return (answerUserTestModel, error);
        }
    }
}
