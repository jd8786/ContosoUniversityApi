using ContosoUniversity.Api.Controllers;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using ContosoUniversity.Data.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Api.Test.Controllers.Students
{
    [Trait("Category", "Unit Test: Api.Controllers.Students.PostStudent")]
    public class PostStudentTests
    {
        private readonly Mock<IStudentsService> _studentService;

        private readonly Mock<IEnrollmentsService> _enrollmentService;

        private readonly StudentsController _controller;

        public PostStudentTests()
        {
            _studentService = new Mock<IStudentsService>();
            _enrollmentService = new Mock<IEnrollmentsService>();
            _controller = new StudentsController(_studentService.Object, _enrollmentService.Object);
        }

        [Fact]
        public void ShouldReturnOkResponseWhenPostingStudentWithNullEnrollments()
        {
            _studentService.Setup(s => s.Add(It.IsAny<Student>())).Returns(new Student { StudentId = 1 });

            _studentService.Setup(s => s.Get(1)).Returns(new Student { StudentId = 1 });

            var response = _controller.PostStudent(new Student());

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            _enrollmentService.Verify(s => s.AddRange(It.IsAny<IEnumerable<Enrollment>>()), Times.Never);

            var responseObject = (ApiResponse<Student>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Should().BeEquivalentTo(new Student { StudentId = 1 });
        }

        [Fact]
        public void ShouldReturnOkResponseWhenPostingStudentWithEmptyEnrollments()
        {
            _studentService.Setup(s => s.Add(It.IsAny<Student>())).Returns(new Student { StudentId = 1 });

            _studentService.Setup(s => s.Get(1)).Returns(new Student { StudentId = 1 });

            var response = _controller.PostStudent(new Student { Enrollments = new List<Enrollment>() });

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            _enrollmentService.Verify(s => s.AddRange(It.IsAny<IEnumerable<Enrollment>>()), Times.Never);

            var responseObject = (ApiResponse<Student>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Should().BeEquivalentTo(new Student { StudentId = 1 });
        }

        [Fact]
        public void ShouldReturnOkResponseWhenPostingStudentWithEnrollments()
        {
            _studentService.Setup(s => s.Add(It.IsAny<Student>())).Returns(new Student { StudentId = 1 });

            _studentService.Setup(s => s.Get(1)).Returns(new Student { StudentId = 1 });

            var student = new Student { Enrollments = new List<Enrollment> { new Enrollment { CourseId = 1 } } };

            var response = _controller.PostStudent(student);

            student.Enrollments.All(s => s.StudentId == 1).Should().BeTrue();

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            _enrollmentService.Verify(s => s.AddRange(student.Enrollments), Times.Exactly(1));

            var responseObject = (ApiResponse<Student>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Should().BeEquivalentTo(new Student { StudentId = 1 });
        }


        [Fact]
        public void ShouldReturnBadRequestWhenThrowingInvalidStudentException()
        {
            _studentService.Setup(s => s.Add(It.IsAny<Student>())).Throws(new InvalidStudentException("some-error-message"));

            var response = _controller.PostStudent(It.IsAny<Student>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(400);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnBadRequestWhenThrowingInvalidEnrollmentException()
        {
            _studentService.Setup(s => s.Add(It.IsAny<Student>())).Returns(new Student { StudentId = 1 });

            _enrollmentService.Setup(s => s.AddRange(It.IsAny<IEnumerable<Enrollment>>())).Throws(new InvalidEnrollmentException("some-error-message"));

            var response =
                _controller.PostStudent(
                    new Student { Enrollments = new List<Enrollment> { new Enrollment { CourseId = 1 } } });

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(400);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnNotFoundWhenThrowingNotFoundException()
        {
            _studentService.Setup(s => s.Add(It.IsAny<Student>())).Returns(new Student { StudentId = 1 });

            _enrollmentService.Setup(s => s.AddRange(It.IsAny<IEnumerable<Enrollment>>())).Throws(new NotFoundException("some-error-message"));

            var response =
                _controller.PostStudent(
                    new Student { Enrollments = new List<Enrollment> { new Enrollment { CourseId = 1 } } });

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(404);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }

        [Fact]
        public void ShouldReturnInternalServerErrorWhenThrowingException()
        {
            _studentService.Setup(s => s.Add(It.IsAny<Student>())).Throws(new Exception("some-error-message"));

            var response = _controller.PostStudent(It.IsAny<Student>());

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(500);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }
    }
}
