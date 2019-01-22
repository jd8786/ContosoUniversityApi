using AutoFixture;
using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Students
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Api.Controllers.Students.PostStudent")]
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
        public async void ShouldReturnOkWhenPostStudentWithoutEnrollments()
        {
            var student = _autoFixture.Build<Student>()
                .With(s => s.LastName, "some-last-name")
                .With(s => s.FirstMidName, "some-first-mid-name")
                .With(s => s.OriginCountry, "some-origin-country")
                .Without(s => s.Courses)
                .Without(s => s.StudentId)
                .Create();

            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PostAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbContext = _fixture.SchoolContext;

            var dbStudent = dbContext.Students.First(s => s.LastName == "some-last-name");

            dbContext.Enrollments.Any(e => e.StudentId == dbStudent.StudentId).Should().BeFalse();
        }

        [Fact]
        public async void ShouldReturnOkWhenPostStudentWithEnrollments()
        {
            var student = _autoFixture.Build<Student>()
                .With(s => s.LastName, "some-last-name")
                .With(s => s.FirstMidName, "some-first-mid-name")
                .With(s => s.OriginCountry, "some-origin-country")
                .With(s => s.Courses, new List<Course> { new Course { CourseId = 1050 }, new Course { CourseId = 4022 } })
                .Without(s => s.StudentId)
                .Create();

            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PostAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbContext = _fixture.SchoolContext;

            var dbStudent = dbContext.Students.First(s => s.LastName == "some-last-name");

            dbContext.Enrollments.Count(e => e.StudentId == dbStudent.StudentId).Should().Be(2);
        }
    }
}
