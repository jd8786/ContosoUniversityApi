using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using Xunit;
using ApiModels = ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Course
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Api.Controllers.Course")]
    public class GetCourseByIdTests
    {
        private readonly AcceptanceTestFixture _fixture;

        public GetCourseByIdTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetDatabase();
        }

        [Fact]
        public async void ShouldReturnOkWhenRetrievingCourseById()
        {
            var course = _fixture.SchoolContext.Courses.First(s => s.Title == "test-title1");

            var courseId = course.CourseId;

            var apiResponse = await _fixture.HttpClient.GetAsync($"api/courses/{courseId}");

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await apiResponse.Content.ReadAsStringAsync();

            var responseOfCourse = JsonConvert.DeserializeObject<ApiResponse<ApiModels.Course>>(content);

            responseOfCourse.Data.CourseId.Should().Be(courseId);
            responseOfCourse.Data.Credits.Should().Be(3);
            responseOfCourse.Data.Title.Should().Be("test-title1");
            responseOfCourse.Data.Department.Name.Should().Be("test-name1");
            responseOfCourse.Data.Instructors.Count().Should().Be(1);
            responseOfCourse.Data.Students.Count().Should().Be(1);
        }
    }
}
