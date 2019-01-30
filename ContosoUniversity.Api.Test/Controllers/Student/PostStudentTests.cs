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

namespace ContosoUniversity.Api.Test.Controllers.Student
{
    [Trait("Category", "Unit Test: Api.Controllers.Student")]
    public class PostStudentTests
    {
        private readonly Mock<IStudentService> _studentService;

        private readonly StudentController _controller;

        public PostStudentTests()
        {
            _studentService = new Mock<IStudentService>();
            _controller = new StudentController(_studentService.Object);
        }

        [Fact]
        public void ShouldReturnOkResponseWhenPostingStudent()
        {
            _studentService.Setup(s => s.Add(It.IsAny<ApiModels.Student>())).Returns(new ApiModels.Student { StudentId = 1 });

            var response = _controller.PostStudent(new ApiModels.Student());

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            var responseObject = (ApiResponse<ApiModels.Student>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Should().BeEquivalentTo(new ApiModels.Student { StudentId = 1 });
        }

        [Fact]
        public void ShouldReturnBadRequestWhenThrowingInvalidStudentException()
        {
            _studentService.Setup(s => s.Add(It.IsAny<ApiModels.Student>())).Throws(new InvalidStudentException("some-error-message"));

            var response = _controller.PostStudent(It.IsAny<ApiModels.Student>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(400);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnNotFoundWhenThrowingNotFoundException()
        {
            _studentService.Setup(s => s.Add(It.IsAny<ApiModels.Student>())).Throws(new NotFoundException("some-error-message"));

            var response = _controller.PostStudent(new ApiModels.Student());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(404);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnInternalServerErrorWhenThrowingException()
        {
            _studentService.Setup(s => s.Add(It.IsAny<ApiModels.Student>())).Throws(new Exception("some-error-message"));

            var response = _controller.PostStudent(It.IsAny<ApiModels.Student>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(500);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }
    }
}
