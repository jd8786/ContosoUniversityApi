using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Xunit;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Students
{
    [Trait("Category", "Acceptance Test: Get Students")]
    public class GetStudentsTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetStudentsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateDefaultClient();
        }

        [Fact]
        public async void ShouldReturnOkWhenRetrievingAllStudents()
        {
            var apiResponse = await _client.GetAsync("api/students");

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await apiResponse.Content.ReadAsStringAsync();

            var apiResponseOfStudents = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<Student>>>(content);

            apiResponseOfStudents.Data.Count().Should().Be(8);
        }
    }
}
