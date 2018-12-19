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
    [Trait("Category", "Unit Test: Api.Controllers.Students.PutStudent")]
    public class PutStudentTests
    {
        private readonly Mock<IStudentsService> _studentService;

        private readonly Mock<IEnrollmentsService> _enrollmentService;

        private readonly StudentsController _controller;

        public PutStudentTests()
        {
            _studentService = new Mock<IStudentsService>();
            _enrollmentService = new Mock<IEnrollmentsService>();
            _controller = new StudentsController(_studentService.Object, _enrollmentService.Object);
        }

        [Fact]
        public void ShouldReturnOkResponse()
        {
            _studentService.Setup(s => s.Update(It.IsAny<Student>())).Returns(new Student { StudentId = 1, LastName = "some-last-name"});

            var response = _controller.PutStudent(new Student { StudentId = 1 });

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            _enrollmentService.Verify(s => s.Update(1, null), Times.Exactly(1));

            var responseObject = (ApiResponse<Student>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Should().BeEquivalentTo(new Student { StudentId = 1, LastName = "some-last-name"});
        }

        [Fact]
        public void ShouldReturnBadRequestWhenThrowingInvalidStudentException()
        {
            _studentService.Setup(s => s.Update(It.IsAny<Student>())).Throws(new InvalidStudentException("some-error-message"));

            var response = _controller.PutStudent(It.IsAny<Student>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(400);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnBadRequestWhenThrowingInvalidEnrollmentException()
        {
            var student = new Student();

            _enrollmentService.Setup(s => s.Update(student.StudentId, student.Enrollments)).Throws(new InvalidEnrollmentException("some-error-message"));

            var response = _controller.PutStudent(student);

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(400);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnNotFoundWhenThrowingNotFoundException()
        {
            var student = new Student();

            _enrollmentService.Setup(s => s.Update(student.StudentId, student.Enrollments)).Throws(new NotFoundException("some-error-message"));

            var response = _controller.PutStudent(student);

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(404);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnInternalServerErrorWhenThrowingException()
        {
            _studentService.Setup(s => s.Update(It.IsAny<Student>())).Throws(new Exception("some-error-message"));

            var response = _controller.PutStudent(It.IsAny<Student>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(500);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }
    }
}
