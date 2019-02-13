using AutoFixture;
using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using ApiModels = ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Course
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Api.Controllers.Course")]
    public class PostCourseTests
    {
        private readonly AcceptanceTestFixture _fixture;

        private readonly Fixture _autoFixture;

        public PostCourseTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetDatabase();
            _autoFixture = new Fixture();
        }

        [Fact]
        public async void ShouldReturnOkWhenPostingCourseWithoutChildren()
        {
            var existingDepartment = _fixture.SchoolContext.Departments.First(d => d.Name == "test-name1");

            var course = _autoFixture.Build<ApiModels.Course>()
                .With(c => c.CourseId, 0)
                .With(c => c.Title, "some-title")
                .With(c => c.Credits, 5)
                .With(c => c.Department, new ApiModels.Department { DepartmentId = existingDepartment.DepartmentId })
                .Without(c => c.Instructors)
                .Without(c => c.Students)
                .Create();

            var courseJson = JsonConvert.SerializeObject(course);

            var content = new StringContent(courseJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PostAsync("api/courses", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbCourse = _fixture.SchoolContext.Courses.First(c => c.Title == "some-title");

            dbCourse.Should().NotBeNull();
            _fixture.SchoolContext.Enrollments.Any(e => e.CourseId == dbCourse.CourseId).Should().BeFalse();
            _fixture.SchoolContext.CourseAssignments.Any(ca => ca.CourseId == dbCourse.CourseId).Should().BeFalse();
            _fixture.SchoolContext.Departments.Any(d => d.Courses.Any(c => c.Title == "some-title")).Should().BeTrue();
        }

        [Fact]
        public async void ShouldReturnOkWhenPostingStudentWithCourses()
        {
            var student = _autoFixture.Build<ApiModels.Student>()
                .With(s => s.StudentId, 0)
                .With(s => s.LastName, "some-last-name")
                .With(s => s.FirstMidName, "some-first-mid-name")
                .With(s => s.OriginCountry, "some-origin-country")
                .With(s => s.Courses, new List<ApiModels.Course> { new ApiModels.Course { CourseId = 1050 }, new ApiModels.Course { CourseId = 4022 } })
                .Create();

            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PostAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbStudent = _fixture.SchoolContext.Students.First(s => s.LastName == "some-last-name");

            _fixture.SchoolContext.Enrollments.Count(e => e.StudentId == dbStudent.StudentId).Should().Be(2);
        }
    }
}
