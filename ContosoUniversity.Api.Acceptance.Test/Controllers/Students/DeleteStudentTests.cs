using System.Linq;
using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Students
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Delete Student")]
    public class DeleteStudentTests
    {
        private readonly AcceptanceTestFixture _fixture;

        public DeleteStudentTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetDatabase();
        }

        [Fact]
        public async void ShouldReturnOkWhenDeleteStudent()
        {
            var student = _fixture.SchoolContext.Students.First(s => s.LastName == "test-last-name1");

            var studentId = student.StudentId;

            var apiResponse = await _fixture.HttpClient.DeleteAsync($"api/students/{studentId}");

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await apiResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<ApiResponse<bool>>(content);

            response.Data.Should().BeTrue();

            var dbStudents = _fixture.SchoolContext.Students.ToList();

            dbStudents.Any(s => s.LastName == "test-last-name1").Should().BeFalse();
        }
    }
}
