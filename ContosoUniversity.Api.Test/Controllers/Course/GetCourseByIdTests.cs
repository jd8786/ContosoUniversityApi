using ContosoUniversity.Api.Controllers;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using ContosoUniversity.Data.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using ApiModels = ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Test.Controllers.Course
{
    [Trait("Category", "Unit Test: Api.Controllers.Course")]
    public class GetCourseByIdTests
    {
        private readonly Mock<ICourseService> _courseService;

        private readonly CourseController _controller;

        public GetCourseByIdTests()
        {
            _courseService = new Mock<ICourseService>();
            _controller = new CourseController(_courseService.Object);
        }

        [Fact]
        public void ShouldReturnOkResponse()
        {
            _courseService.Setup(c => c.Get(It.IsAny<int>())).Returns(new ApiModels.Course());

            var response = _controller.GetCourseById(1);

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            var responseObject = (ApiResponse<ApiModels.Course>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Should().BeEquivalentTo(new ApiModels.Course());
        }

        [Fact]
        public void ShouldReturnNotFoundWhenThrowingNotFoundException()
        {
            _courseService.Setup(c => c.Get(It.IsAny<int>())).Throws(new NotFoundException("some-error-message"));

            var response = _controller.GetCourseById(1);

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(404);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnInternalServerErrorWhenThrowingException()
        {
            _courseService.Setup(c => c.Get(It.IsAny<int>())).Throws(new Exception("some-error-message"));

            var response = _controller.GetCourseById(1);

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(500);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string>{"some-error-message"});
        }
    }
}
