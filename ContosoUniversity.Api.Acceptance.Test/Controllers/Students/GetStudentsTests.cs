using System.Net;
using System.Net.Http;
using AutoMapper;
using ContosoUniversity.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.Storage.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Students
{
    public class GetStudentsTests: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetStudentsTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.WithWebHostBuilder(bulider => bulider.ConfigureServices(services => { // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (ApplicationDbContext) using an in-memory 
                // database for testing.
                services.AddDbContext<SchoolContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();
            })).CreateClient();
        }

        [Fact]
        public async void ShouldReturnOkWhenRetrievingAllStudents()
        {
            var response = await _client.GetAsync("api/students");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
