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

namespace ContosoUniversity.Api.Test.Controllers.Students
{
    [Trait("Category", "Unit Test: Api.Controllers.Students.GetStudentById")]
    public class GetStudentByIdTests
    {
        private readonly Mock<IStudentsService> _studentService;

        private readonly StudentsController _controller;

        public GetStudentByIdTests()
        {
            _studentService = new Mock<IStudentsService>();
            _controller = new StudentsController(_studentService.Object, null);
        }

        [Fact]
        public void ShouldReturnOkResponse()
        {
            _studentService.Setup(s => s.Get(It.IsAny<int>())).Returns(new Student());

            var response = _controller.GetStudentById(It.IsAny<int>());

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            var responseObject = (ApiResponse<Student>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Should().BeEquivalentTo(new Student());
        }

        [Fact]
        public void ShouldReturnNotFoundWhenThrowingNotFoundException()
        {
            _studentService.Setup(s => s.Get(It.IsAny<int>())).Throws(new NotFoundException("some-error-message"));

            var response = _controller.GetStudentById(It.IsAny<int>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(404);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnInternalServerErrorWhenThrowingException()
        {
            _studentService.Setup(s => s.Get(It.IsAny<int>())).Throws(new Exception("some-error-message"));

            var response = _controller.GetStudentById(It.IsAny<int>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(500);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string>{"some-error-message"});
        }
    }
}
