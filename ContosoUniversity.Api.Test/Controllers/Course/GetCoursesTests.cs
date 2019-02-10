using ContosoUniversity.Api.Controllers;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using ApiModels = ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Test.Controllers.Course
{
    [Trait("Category", "Unit Test: Api.Controllers.Course")]
    public class GetCoursesTests
    {
        private readonly Mock<ICourseService> _courseService;

        private readonly CourseController _controller;

        public GetCoursesTests()
        {
            _courseService = new Mock<ICourseService>();
            _controller = new CourseController(_courseService.Object);
        }

        [Fact]
        public void ShouldReturnOkResponse()
        {
            _courseService.Setup(c => c.GetAll()).Returns(new List<ApiModels.Course> { new ApiModels.Course() });

            var response = _controller.GetCourses();

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            var responseObject = (ApiResponse<IEnumerable<ApiModels.Course>>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Count().Should().Be(1);
        }

        [Fact]
        public void ShouldReturnInternalServerErrorWhenThrowingException()
        {
            _courseService.Setup(c => c.GetAll()).Throws(new Exception("some-error-message"));

            var response = _controller.GetCourses();

            var errorResponse = (ObjectResult)response;

            errorResponse.StatusCode.Should().Be(500);

            var responseObject = (ApiResponse<bool>)errorResponse.Value;

            responseObject.IsSuccess.Should().BeFalse();

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }
    }
}
