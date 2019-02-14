using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;
using ApiModels = ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Course
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Api.Controllers.Course")]
    public class GetCoursesTests
    {
        private readonly AcceptanceTestFixture _fixture;

        public GetCoursesTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetDatabase();
        }

        [Fact]
        public async void ShouldReturnOkWhenRetrievingAllCourses()
        {
            var apiResponse = await _fixture.HttpClient.GetAsync("api/courses");

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await apiResponse.Content.ReadAsStringAsync();

            var responseOfCourses = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<ApiModels.Course>>>(content);

            responseOfCourses.IsSuccess.Should().BeTrue();
            responseOfCourses.Data.Count().Should().Be(2);
        }
    }
}
