using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;

namespace DrivingSchool.MockData.Repositories
{
    public class AnswerUserTestRepositoryMock
    {
        public void Dispose()
        {
        }

        public async Task<Guid> CreateAsync(AnswerUserTestModel answer)
        {
            var answerUserTestEntity = new AnswerUserTestEntity
            {
                Id = answer.Id,
                User = new UserEntity
                {
                    Id = answer.User!.Id,
                    UserName = answer.User.UserName,
                    Email = answer.User.Email,
                    Password = answer.User.Password,
                    Role = answer.User.Role
                },
                Test = new TestEntity
                {
                    Id = answer.Test!.Id,
                    NameTest = answer.Test.NameTest
                },
                ResultTest = answer.ResultTest,
            };

            return await answerUserTestEntity.Id.AsTask();
        }

        public async Task<List<AnswerUserTestModel>> GetAsync()
        {
            var usersEntities = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = "UserName",
                Email = "Email",
                Password = "password",
                Role = "admin",
            };

            var testEntity = new TestEntity
            {
                Id = Guid.NewGuid(),
                NameTest = "Test"
            };

            var answerUserTestEntity = new List<AnswerUserTestEntity>
            {
                new AnswerUserTestEntity
                {
                    Id = Guid.NewGuid(),
                    User = usersEntities,
                    Test = testEntity,
                    ResultTest = 0
                }
            };

            var answerUserTests = answerUserTestEntity
                .Select(a =>
                AnswerUserTestModel.Create(a.Id, UserModel.Create(a.User.Id, a.User.UserName, a.User.Email, a.User.Password, a.User.Role).user,
                TestModel.Create(a.Test.Id, a.Test.NameTest).test,
                a.ResultTest).answer).ToList();

            return await answerUserTests.AsTask();
        }

        public async Task<Guid> UpdateAsync(Guid id, Guid idUser, Guid idTest, int resultTest)
        {
            return await id.AsTask();
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            return await id.AsTask();
        }
    }
}
