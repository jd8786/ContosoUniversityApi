using System;
using System.Collections.Generic;
using ContosoUniversity.Api.Controllers;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using ContosoUniversity.Data.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ContosoUniversity.Api.Test.Controllers.Student
{
    [Trait("Category", "Unit Test: Api.Controllers.Student.DeleteStudent")]
    public class DeleteStudentTests
    {
        private readonly Mock<IStudentService> _studentService;

        private readonly StudentController _controller;

        public DeleteStudentTests()
        {
            _studentService = new Mock<IStudentService>();
            _controller = new StudentController(_studentService.Object);
        }

        [Fact]
        public void ShouldReturnOkResponse()
        {
            var response = _controller.DeleteStudent(1);

            _studentService.Verify(s => s.Remove(1), Times.Exactly(1));

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            var responseObject = (ApiResponse<bool>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Should().BeTrue();
        }

        [Fact]
        public void ShouldReturnNotFoundWhenThrowingNotFoundException()
        {
            _studentService.Setup(s => s.Remove(It.IsAny<int>())).Throws(new NotFoundException("some-error-message"));

            var response = _controller.DeleteStudent(It.IsAny<int>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(404);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnInternalServerErrorWhenThrowingException()
        {
            _studentService.Setup(s => s.Remove(It.IsAny<int>())).Throws(new Exception("some-error-message"));

            var response = _controller.DeleteStudent(It.IsAny<int>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(500);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string>{"some-error-message"});
        }
    }
}
