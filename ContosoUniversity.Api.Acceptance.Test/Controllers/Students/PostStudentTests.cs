using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using Xunit;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Students
{
    [Trait("Category", "Acceptance Test: Get Student By Id")]
    public class GetStudentByIdTests: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetStudentByIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateDefaultClient();
        }

        [Fact]
        public async void ShouldReturnOkWhenRetrievingStudentById()
        {
            var apiResponse = await _client.GetAsync("api/students/3");

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await apiResponse.Content.ReadAsStringAsync();

            var responseOfStudent = JsonConvert.DeserializeObject<ApiResponse<Student>>(content);

            responseOfStudent.Data.StudentId.Should().Be(3);
            responseOfStudent.Data.LastName.Should().Be("Anand");
            responseOfStudent.Data.FirstMidName.Should().Be("Arturo");
            responseOfStudent.Data.OriginCountry.Should().Be("TURKEY");
        }
    }
}
