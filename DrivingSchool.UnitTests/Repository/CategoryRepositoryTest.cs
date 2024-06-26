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
            var albums = await _repository.Get();

            // Assert
            Assert.Single(albums);
        }
    }
}
