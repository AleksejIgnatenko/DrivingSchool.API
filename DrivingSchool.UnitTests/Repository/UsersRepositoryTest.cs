using DrivingSchool.Core.Models;
using DrivingSchool.MockData.Repositories;
using JetBrains.dotMemoryUnit;

namespace DrivingSchool.UnitTests.Repository
{
    public class UsersRepositoryTest
    {
        private readonly UsersRepositoryMock _repository;

        public UsersRepositoryTest()
        {
            _repository = new UsersRepositoryMock();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            UserModel user = UserModel.Create(Guid.NewGuid(), "UserName", "email", "password", "role").user;

            // Act
            var id = await _repository.CreateAsync(user);

            // Assert
            Assert.Equal(user.Id, id);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task GetAsync()
        {
            // Arrange

            // Act
            var users = await _repository.GetAsync();

            // Assert
            Assert.Single(users);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            Guid id = Guid.Parse("87742b94-99f5-4ecd-9c0d-0676d9ae3cc4");
            // Act
            var user = await _repository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(user);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string userName = "update";
            string email = "update";
            string password = "update";
            string role = "update";

            // Act
            var id = await _repository.UpdateAsync(userId, userName, email, password, role);
            // Assert
            Assert.Equal(userId, id);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task DeleteAsync()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            // Act
            var id = await _repository.DeleteAsync(userId);

            // Assert
            Assert.Equal(userId, id);
        }
    }
}
