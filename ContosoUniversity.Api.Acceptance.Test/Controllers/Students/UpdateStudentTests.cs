using AutoFixture;
using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Students
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Update Student")]
    public class UpdateStudentTests
    {
        private readonly AcceptanceTestFixture _fixture;

        private readonly Fixture _autoFixture;

        public UpdateStudentTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetDatabase();
            _autoFixture = new Fixture();
        }

        [Fact]
        public async void ShouldReturnOkWhenUpdateStudentWithoutSelectedCourses()
        {
            var existingStudent = _fixture.SchoolContext.Students
                .Include(s => s.Enrollments)
                .First(s => s.LastName == "test-last-name1");

            existingStudent.Enrollments.Count.Should().Be(1);

            var studentId = existingStudent.StudentId;

            var student = _autoFixture.Build<Student>()
                .With(s => s.StudentId, studentId)
                .With(s => s.LastName, "some-last-name")
                .With(s => s.FirstMidName, "some-first-mid-name")
                .With(s => s.OriginCountry, "some-origin-country")
                .Without(s => s.Enrollments)
                .Create();

            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PutAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbContext = _fixture.SchoolContext;

            var dbStudent = dbContext.Students.First(s => s.StudentId == studentId);

            dbStudent.LastName.Should().Be("some-last-name");
            dbStudent.FirstMidName.Should().Be("some-first-mid-name");
            dbStudent.OriginCountry.Should().Be("some-origin-country");
            dbStudent.Enrollments.Should().BeNull();
        }
    }
}
