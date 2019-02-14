using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;
using ApiModels = ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Student
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Api.Controllers.Student")]
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

            var responseOfStudents = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<ApiModels.Student>>>(content);

            responseOfStudents.IsSuccess.Should().BeTrue();
            responseOfStudents.Data.Count().Should().Be(2);
        }
    }
}
