using AutoFixture;
using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Data.EntityModels;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using ApiModels = ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Student
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Api.Controllers.Student")]
    public class UpdateStudentTests
    {
        private readonly AcceptanceTestFixture _fixture;

        private readonly Fixture _autoFixture;

        public UpdateStudentTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetDatabase();
            _autoFixture = new Fixture();
        }

        [Fact]
        public async void ShouldReturnOkWhenUpdatingStudent()
        {
            var existingStudent = _fixture.SchoolContext.Students
                .Include(s => s.Enrollments)
                .First(s => s.LastName == "test-last-name1");

            var studentId = existingStudent.StudentId;

            var courses = new List<ApiModels.Course>
            {
                new ApiModels.Course {CourseId = 4022},
                new ApiModels.Course {CourseId = 1050, Grade = Grade.F}
            };

            var student = _autoFixture.Build<ApiModels.Student>()
                .With(s => s.StudentId, studentId)
                .With(s => s.LastName, "some-last-name")
                .With(s => s.FirstMidName, "some-first-mid-name")
                .With(s => s.OriginCountry, "some-origin-country")
                .With(s => s.EnrollmentDate, new DateTime(2018, 1, 1))
                .With(s => s.Courses, courses)
                .Create();

            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PutAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseOfContent = await apiResponse.Content.ReadAsStringAsync();

            var responseOfStudent = JsonConvert.DeserializeObject<ApiModels.ApiResponse<ApiModels.Student>>(responseOfContent);

            responseOfStudent.IsSuccess.Should().BeTrue();
            responseOfStudent.Data.LastName.Should().Be("some-last-name");
            responseOfStudent.Data.FirstMidName.Should().Be("some-first-mid-name");
            responseOfStudent.Data.OriginCountry.Should().Be("some-origin-country");
            responseOfStudent.Data.EnrollmentDate.Should().Be(new DateTime(2018, 1, 1));
            responseOfStudent.Data.Courses.Count().Should().Be(2);
            _fixture.SchoolContext.Enrollments.Single(e => e.StudentId == studentId && e.CourseId == 1050).Grade.Should().Be(Grade.F);
        }
    }
}
