using DrivingSchool.MockData.Repositories;
using JetBrains.dotMemoryUnit;

namespace DrivingSchool.UnitTests.Repository
{
    public class CategoryRepositoryTest
    {
        private readonly CategoryRepositoryMock _repository;

        public CategoryRepositoryTest()
        {
            _repository = new CategoryRepositoryMock();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task GetAsync()
        {
            // Arrange

            // Act
            var category = await _repository.GetAsync();

            // Assert
            Assert.Single(category);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            Guid id = Guid.Parse("d833d875-660f-4d6b-a138-796a6ae98095");

            // Act
            var category = await _repository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(category);
        }
    }
}
