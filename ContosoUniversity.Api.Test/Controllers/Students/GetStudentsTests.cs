using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ContosoUniversity.Api.Controllers;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Xunit;

namespace ContosoUniversity.Api.Test.Controllers.Students
{
    [Trait("Category", "Unit Test: Api.Controllers.Students.GetStudents")]
    public class GetStudentsTests
    {
        private readonly Mock<IStudentsService> _studentService;

        private readonly Mock<IEnrollmentsService> _enrollmentService;

        private readonly StudentsController _controller;

        public GetStudentsTests()
        {
            _studentService = new Mock<IStudentsService>();
            _enrollmentService = new Mock<IEnrollmentsService>();
            _controller = new StudentsController(_studentService.Object, _enrollmentService.Object);
        }

        [Fact]
        public void ShouldReturnOkResponse()
        {
            _studentService.Setup(s => s.GetAll()).Returns(new List<Student>());

            var response = _controller.GetStudents();

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            var responseObject = (ApiResponse<IEnumerable<Student>>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Count().Should().Be(0);
        }

        [Fact]
        public void ShouldReturnInternalServerErrorWhenThrowingException()
        {
            _studentService.Setup(s => s.GetAll()).Throws(new Exception("some-error-message"));

            var response = _controller.GetStudents();

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(500);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string>{"some-error-message"});
        }
    }
}
