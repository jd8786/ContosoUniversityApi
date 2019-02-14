using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using Xunit;
using ApiModels = ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Student
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Api.Controllers.Student")]
    public class GetStudentByIdTests
    {
        private readonly AcceptanceTestFixture _fixture;

        public GetStudentByIdTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetDatabase();
        }

        [Fact]
        public async void ShouldReturnOkWhenRetrievingStudentById()
        {
            var student = _fixture.SchoolContext.Students.First(s => s.LastName == "test-last-name1");

            var studentId = student.StudentId;

            var apiResponse = await _fixture.HttpClient.GetAsync($"api/students/{studentId}");

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await apiResponse.Content.ReadAsStringAsync();

            var responseOfStudent = JsonConvert.DeserializeObject<ApiResponse<ApiModels.Student>>(content);

            responseOfStudent.IsSuccess.Should().BeTrue();
            responseOfStudent.Data.StudentId.Should().Be(studentId);
            responseOfStudent.Data.LastName.Should().Be("test-last-name1");
            responseOfStudent.Data.FirstMidName.Should().Be("test-first-mid-name1");
            responseOfStudent.Data.OriginCountry.Should().Be("test-country1");
            responseOfStudent.Data.EnrollmentDate.Should().Be(new DateTime(2010, 9, 1));
            responseOfStudent.Data.Courses.Count().Should().Be(1);
        }
    }
}
