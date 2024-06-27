

using DrivingSchool.Core.Models;
using DrivingSchool.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.DataAccess.Repositories
{
    public class AnswerUserTestRepository
    {
        private readonly DrivingSchoolDbContext _context;

        public AnswerUserTestRepository(DrivingSchoolDbContext context)
        {
            this._context = context;
        }

        public async Task<Guid> CreateAsync(AnswerUserTestModel answerUserTestModel)
        {
            UserEntity userEntity = new UserEntity
            {
                Id = answerUserTestModel.User.Id,
                UserName = answerUserTestModel.User.UserName,
                Email = answerUserTestModel.User.Email,
                Password = answerUserTestModel.User.Password,
                Role = answerUserTestModel.User.Role
            };

            CategoryEntity categoryEntity = new CategoryEntity
            {
                Id = answerUserTestModel.Test.Category.Id,
                NameCategory = answerUserTestModel.Test.Category.NameCategory
            };

            TestEntity testEntity = new TestEntity
            {
                Id = answerUserTestModel.Test.Id,
                Category = categoryEntity,
                NameTest = answerUserTestModel.Test.NameTest
            };

            AnswerUserTestEntity answerUserTestEntity = new AnswerUserTestEntity
            {
                Id = answerUserTestModel.Id,
                User = userEntity,
                Test = testEntity,
                ResultTest = answerUserTestModel.ResultTest
            };

            await _context.AnswerUserTests.AddAsync(answerUserTestEntity);
            await _context.SaveChangesAsync();

            return answerUserTestEntity.Id;
        }

        public async Task<List<AnswerUserTestModel>> GetAsync()
        {
            var answerUserTestEntity = await _context.AnswerUserTests
                .AsNoTracking()
                .Include(a => a.User)
                .Include(a => a.Test)
                .ToListAsync();

            var answerUserTests = answerUserTestEntity
                .Select(a =>
                AnswerUserTestModel.Create(a.Id, UserModel.Create(a.User.Id, a.User.UserName, a.User.Email, a.User.Password, a.User.Role).user,
                TestModel.Create(a.Test.Id, a.Test.NameTest).test,
                a.ResultTest).answerUserTestModel).ToList();

            return answerUserTests;
        }

        public async Task<Guid> UpdateAsync(Guid id, Guid idUser, Guid idTest, int resultTest)
        {
            var answerUserTest = _context.AnswerUserTests.Find(id);
            var userEntity = _context.Users.Find(idUser);
            var testEntity = _context.Tests.Find(idTest);

            if ((answerUserTest != null) && (userEntity != null) && (testEntity != null))
            {
                answerUserTest.User = userEntity;
                answerUserTest.Test = testEntity;
                answerUserTest.ResultTest = resultTest;

                await _context.SaveChangesAsync();

                return id;
            }

            throw new Exception("Error for update answer");
        }

        public async Task<Guid> DeleteAsync(Guid id)
        {
            await _context.AnswerUserTests
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
