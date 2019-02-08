using System.Collections.Generic;
using System.Linq;
using System.Net;
using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
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

            var apiResponseOfCourses = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<ApiModels.Course>>>(content);

            apiResponseOfCourses.Data.Count().Should().Be(2);
        }
    }
}
