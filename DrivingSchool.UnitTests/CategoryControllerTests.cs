using DrivingSchool.API.Contracts.CategoryContracts;
using DrivingSchool.API.Controllers;
using DrivingSchool.BusinessLogic.CategoryServices;
using DrivingSchool.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace DrivingSchool.API.UnitTests
{
    [TestFixture]
    public class CategoryControllerTests
    {
        [Fact]
        public async Task CreateCategory_ReturnsOkResult_WithCategoryId()
        {
            // Arrange
            var categoryRequest = new CategoryRequest("Test Category");

            var categoryServicesMock = new Mock<ICategoryServices>();
            var categoryId = Guid.NewGuid();

            categoryServicesMock.Setup(s => s.CreateCategory(It.IsAny<CategoryModel>()))
                .ReturnsAsync(categoryId);

            var controller = new CategoryController(categoryServicesMock.Object);

            // Act
            var result = await controller.CreateCategory(categoryRequest);

            // Assert
            var okObjectResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
            var resultCategoryId = Xunit.Assert.IsType<Guid>(okObjectResult.Value);

            Xunit.Assert.Equal(categoryId, resultCategoryId);
        }

/*        [Fact]
        public async Task CreateCategory_ReturnsBadRequest_WhenErrorOccurs()
        {
            // Arrange
            var categoryRequest = new CategoryRequest("Test Category");

            var categoryServicesMock = new Mock<ICategoryServices>();
            var error = "Error message";

            categoryServicesMock.Setup(s => s.CreateCategory(It.IsAny<CategoryModel>()))
                .ReturnsAsync(Guid.Empty);

            var controller = new CategoryController(categoryServicesMock.Object);

            // Act
            var result = await controller.CreateCategory(categoryRequest);

            // Assert
            var badRequestObjectResult = Xunit.Assert.IsType<BadRequestObjectResult>(result.Result);
            var errorMessage = Xunit.Assert.IsType<string>(badRequestObjectResult.Value);

            Xunit.Assert.Equal(error, errorMessage);
        }*/
    }
}