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
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Api.Controllers.Students.GetStudents")]
    public class GetStudentsTests
    {
        private readonly AcceptanceTestFixture _fixture;

        public GetStudentsTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetDatabase();
        }

        [Fact]
        public async void ShouldReturnOkWhenRetrievingAllStudents()
        {
            var apiResponse = await _fixture.HttpClient.GetAsync("api/students");

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await apiResponse.Content.ReadAsStringAsync();

            var apiResponseOfStudents = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Student>>>(content);

            apiResponseOfStudents.Data.Count().Should().Be(2);
        }
    }
}
