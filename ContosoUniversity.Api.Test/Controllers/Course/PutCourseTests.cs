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
    public class PutCourseTests
    {
        private readonly Mock<ICourseService> _courseService;

        private readonly CourseController _controller;

        public PutCourseTests()
        {
            _courseService = new Mock<ICourseService>();
            _controller = new CourseController(_courseService.Object);
        }

        [Fact]
        public void ShouldReturnOkResponseWhenPuttingCourse()
        {
            _courseService.Setup(c => c.Update(It.IsAny<ApiModels.Course>())).Returns(new ApiModels.Course { CourseId = 1 });

            var response = _controller.PutCourse(new ApiModels.Course());

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            var responseObject = (ApiResponse<ApiModels.Course>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Should().BeEquivalentTo(new ApiModels.Course { CourseId = 1 });
        }

        [Fact]
        public void ShouldReturnBadRequestWhenThrowingInvalidCourseException()
        {
            _courseService.Setup(c => c.Update(It.IsAny<ApiModels.Course>())).Throws(new InvalidCourseException("some-error-message"));

            var response = _controller.PutCourse(It.IsAny<ApiModels.Course>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(400);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnNotFoundWhenThrowingNotFoundException()
        {
            _courseService.Setup(c => c.Update(It.IsAny<ApiModels.Course>())).Throws(new NotFoundException("some-error-message"));

            var response = _controller.PutCourse(new ApiModels.Course());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(404);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnInternalServerErrorWhenThrowingException()
        {
            _courseService.Setup(c => c.Update(It.IsAny<ApiModels.Course>())).Throws(new Exception("some-error-message"));

            var response = _controller.PutCourse(It.IsAny<ApiModels.Course>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(500);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }
    }
}
