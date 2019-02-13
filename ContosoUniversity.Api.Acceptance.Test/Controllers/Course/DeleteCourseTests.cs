using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using Xunit;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Course
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Api.Controllers.Course")]
    public class DeleteCourseTests
    {
        private readonly AcceptanceTestFixture _fixture;

        public DeleteCourseTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetDatabase();
        }

        [Fact]
        public async void ShouldReturnOkWhenDeletingCourse()
        {
            var course = _fixture.SchoolContext.Courses.First(c => c.Title == "test-title1");

            var courseId = course.CourseId;

            var apiResponse = await _fixture.HttpClient.DeleteAsync($"api/courses/{courseId}");

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await apiResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<ApiResponse<bool>>(content);

            response.Data.Should().BeTrue();

            _fixture.SchoolContext.Courses.ToList().Any(c => c.Title == "test-title1").Should().BeFalse();
        }
    }
}
