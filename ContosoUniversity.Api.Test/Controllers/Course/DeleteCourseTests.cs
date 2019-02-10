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

namespace ContosoUniversity.Api.Test.Controllers.Course
{
    [Trait("Category", "Unit Test: Api.Controllers.Course")]
    public class DeleteCourseTests
    {
        private readonly Mock<ICourseService> _courseService;

        private readonly CourseController _controller;

        public DeleteCourseTests()
        {
            _courseService = new Mock<ICourseService>();
            _controller = new CourseController(_courseService.Object);
        }

        [Fact]
        public void ShouldReturnOkResponse()
        {
            var response = _controller.DeleteCourse(1);

            _courseService.Verify(c => c.Remove(1), Times.Exactly(1));

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            var responseObject = (ApiResponse<bool>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnNotFoundWhenThrowingNotFoundException()
        {
            _courseService.Setup(c => c.Remove(It.IsAny<int>())).Throws(new NotFoundException("some-error-message"));

            var response = _controller.DeleteCourse(1);

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(404);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnInternalServerErrorWhenThrowingException()
        {
            _courseService.Setup(c => c.Remove(It.IsAny<int>())).Throws(new Exception("some-error-message"));

            var response = _controller.DeleteCourse(1);

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(500);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string>{"some-error-message"});
        }
    }
}
