﻿using ContosoUniversity.Api.Controllers;
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

namespace ContosoUniversity.Api.Test.Controllers.Student
{
    [Trait("Category", "Unit Test: Api.Controllers.Student")]
    public class GetStudentsTests
    {
        private readonly Mock<IStudentService> _studentService;

        private readonly StudentController _controller;

        public GetStudentsTests()
        {
            _studentService = new Mock<IStudentService>();
            _controller = new StudentController(_studentService.Object);
        }

        [Fact]
        public void ShouldReturnOkResponse()
        {
            _studentService.Setup(s => s.GetAll()).Returns(new List<ApiModels.Student> { new ApiModels.Student() });

            var response = _controller.GetStudents();

            var okResponse = (OkObjectResult)response;

            okResponse.StatusCode.Should().Be(200);

            var responseObject = (ApiResponse<IEnumerable<ApiModels.Student>>)okResponse.Value;

            responseObject.IsSuccess.Should().BeTrue();

            responseObject.Data.Count().Should().Be(1);
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

            responseObject.Messages.Should().BeEquivalentTo(new List<string> { "some-error-message" });
        }
    }
}
