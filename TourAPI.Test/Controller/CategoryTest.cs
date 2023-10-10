using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using OneOf.Types;
using TourAPI.Controllers;
using TourAPI.Models.Domains;
using TourAPI.Models.Dto;
using TourAPI.Models.Request;
using TourAPI.Services;

namespace TourAPI.Test.Controller
{
    public class CategoryTest
    {
        private readonly ICategoryService service;
        private readonly CategoryController controller;
        public CategoryTest()
        {
            service = A.Fake<ICategoryService>();
            controller = new CategoryController(service);
        }

        [Fact]
        public async Task GetAllAsync_ReturnSuccess()
        {
            //Arrange
            var categories = A.Fake<List<Category>>();
            var categoriesResponse = A.Fake<List<CategoryResponse>>();

            A.CallTo(() => service.GetAllAsync()).Returns(categories);

            //Act
            var result = await controller.GetAllAsync();
            var okResult = (OkObjectResult)result;
            var responseList = okResult.Value;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            responseList.Should().BeOfType<List<CategoryResponse>>();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnSuccess()
        {
            string Id = "";
            var category = A.Fake<Category>();
            A.CallTo(() => service.GetByIdAsync(Id))
                .Returns<OneOf<Category, NotFound>>(category);

            var result = await controller.GetByIdAsync(Id);
            var okResult = (OkObjectResult)result;
            var response = okResult.Value;

            result.Should().BeOfType<OkObjectResult>();
            response.Should().BeOfType<CategoryResponse>();
        }

        [Fact]
        public async Task GetByIdAsync_ReturnNotFound()
        {
            string Id = "";
            var category = A.Fake<Category>();
            A.CallTo(() => service.GetByIdAsync(Id))
                .Returns<OneOf<Category, NotFound>>(new NotFound());

            var result = await controller.GetByIdAsync(Id);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CreateAsync_ReturnSuccess()
        {
            var entity = A.Fake<Category>();
            var entityRequest = A.Fake<CategoryRequest>();
            var entityResponse = A.Fake<CategoryResponse>();
            A.CallTo(() => service.CreateAsync(entity)).Returns(Task.FromResult(true));

            var result = await controller.CreateAsync(entityRequest);
            //var okResult = (OkObjectResult)result;
            //var response = okResult.Value;

            result.Should().BeOfType<OkObjectResult>();
            //response.Should().BeOfType<CategoryResponse>();

        }

        [Fact]
        public async Task CreateAsync_ReturnBadRequest()
        {
            var entity = A.Fake<Category>();
            var entityRequest = A.Fake<CategoryRequest>();
            var entityResponse = A.Fake<CategoryResponse>();
            A.CallTo(() => service.CreateAsync(entity)).Returns(false);

            var result = await controller.CreateAsync(entityRequest);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
