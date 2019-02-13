using AutoFixture;
using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ApiModels = ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Student
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Api.Controllers.Student")]
    public class PostStudentTests
    {
        private readonly AcceptanceTestFixture _fixture;

        private readonly Fixture _autoFixture;

        public PostStudentTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetDatabase();
            _autoFixture = new Fixture();
        }

        [Fact]
        public async void ShouldReturnOkWhenPostingStudentWithoutCourses()
        {
            var student = _autoFixture.Build<ApiModels.Student>()
                .With(s => s.StudentId, 0)
                .With(s => s.LastName, "some-last-name")
                .With(s => s.FirstMidName, "some-first-mid-name")
                .With(s => s.OriginCountry, "some-origin-country")
                .Without(s => s.Courses)
                .Create();

            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PostAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbStudent = _fixture.SchoolContext.Students.FirstOrDefault(s => s.LastName == "some-last-name");

            dbStudent.Should().NotBeNull();
        }

        [Fact]
        public async void ShouldReturnOkWhenPostingStudentWithCourses()
        {
            var student = _autoFixture.Build<ApiModels.Student>()
                .With(s => s.StudentId, 0)
                .With(s => s.LastName, "some-last-name")
                .With(s => s.FirstMidName, "some-first-mid-name")
                .With(s => s.OriginCountry, "some-origin-country")
                .With(s => s.Courses, new List<ApiModels.Course> { new ApiModels.Course { CourseId = 1050 }, new ApiModels.Course { CourseId = 4022 } })
                .Create();

            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PostAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbStudent = _fixture.SchoolContext.Students.Include(s => s.Enrollments).FirstOrDefault(s => s.LastName == "some-last-name");

            dbStudent.Should().NotBeNull();
            dbStudent.Enrollments.Count.Should().Be(2);
        }
    }
}
