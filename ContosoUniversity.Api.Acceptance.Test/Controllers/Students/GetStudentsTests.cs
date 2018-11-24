using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Students
{
    [Trait("Category", "Acceptance Test: Get Students")]
    public class GetStudentsTests: IClassFixture<AcceptanceTestFixture>
    {
        private readonly AcceptanceTestFixture _fixture;

        public GetStudentsTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void ShouldReturnOkWhenRetrievingAllStudents()
        {
            var apiResponse = await _fixture.HttpClient.GetAsync("api/students");

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await apiResponse.Content.ReadAsStringAsync();

            var apiResponseOfStudents = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Student>>>(content);

            apiResponseOfStudents.Data.Count().Should().Be(8);
        }
    }
}
