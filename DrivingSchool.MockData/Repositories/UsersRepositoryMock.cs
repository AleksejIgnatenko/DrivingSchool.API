using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;

namespace DrivingSchool.MockData.Repositories
{
    public class UsersRepositoryMock
    {
        public void Dispose()
        {
        }

        public async Task<Guid> CreateAsync(UserModel user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
            };

            return await userEntity.Id.AsTask();
        }

        public async Task<List<UserModel>> GetAsync()
        {
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
                    Test = testEntity,
                    ResultTest = 0
                }
            };

            var usersEntities = new List<UserEntity>
            {
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    AnswerUserTests = answerUserTestEntity,
                    UserName = "UserName",
                    Email = "Email",
                    Password = "password",
                    Role = "admin",
                }
            };

            var users = usersEntities.Select(u =>
                UserModel.Create(
                    u.Id,
                    u.AnswerUserTests.Select(a => AnswerUserTestModel.Create(a.Id, TestModel.Create(a.Test.Id, a.Test.NameTest).test, a.ResultTest).answer).ToList(),
                    u.UserName,
                    u.Email,
                    u.Password,
                    u.Role
                ).user
            ).ToList();

            return await users.AsTask();
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            var usersEntities = new UserEntity
            {
                Id = Guid.Parse("87742b94-99f5-4ecd-9c0d-0676d9ae3cc4"),
                UserName = "UserName",
                Email = "Email",
                Password = "password",
                Role = "admin",
            };

            var user = UserModel.Create(usersEntities.Id, usersEntities.UserName, usersEntities.Email, usersEntities.Password, usersEntities.Role).user;

            return await user.AsTask();
        }

        public async Task<Guid> UpdateAsync(Guid idUser, string username, string email, string password, string role)
        {
            return await idUser.AsTask();
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            return await id.AsTask();
        }
    }
}
