using DrivingSchool.Core.Models;
using DrivingSchool.MockData.Repositories;
using JetBrains.dotMemoryUnit;

namespace DrivingSchool.UnitTests.Repository
{
    public class TestRepositoryTest
    {
        private TestRepositoryMock _repository;

        public TestRepositoryTest()
        {
            _repository = new TestRepositoryMock();
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            CategoryModel category = CategoryModel.Create(Guid.NewGuid(), "Category").category;
            TestModel test = TestModel.Create(Guid.NewGuid(), category, "Test").test;
            
            // Act
            var id = await _repository.CreateAsync(test);

            // Assert
            Assert.Equal(test.Id, id);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task GetAsync()
        {
            // Arrange

            // Act
            var tests = await _repository.GetAsync();

            // Assert
            Assert.Single(tests);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task GetByIdAsync()
        {
            // Arrange
            Guid id = Guid.Parse("5f51c061-b5f8-4579-928e-3858cd3013f5");
            // Act
            var test = await _repository.GetByIdAsync(id);

            // Assert
            Assert.NotNull(test);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            Guid testId = Guid.NewGuid();
            Guid categoryId = Guid.NewGuid();
            string nameTest = "Test";

            // Act
            var id = await _repository.UpdateAsync(testId, categoryId, nameTest);

            // Assert
            Assert.Equal(testId, id);
        }

        [DotMemoryUnit(FailIfRunWithoutSupport = false)]
        [Fact]
        public async Task DeleteAsync()
        {
            // Arrange
            Guid testId = Guid.NewGuid();

            // Act
            var id = await _repository.DeleteAsync(testId);

            // Assert
            Assert.Equal(testId, id);
        }
    }
}
