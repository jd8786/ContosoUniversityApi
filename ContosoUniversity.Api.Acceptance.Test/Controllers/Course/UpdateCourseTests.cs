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
    public class UpdateCourseTests
    {
        private readonly AcceptanceTestFixture _fixture;

        private readonly Fixture _autoFixture;

        public UpdateCourseTests(AcceptanceTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.ResetDatabase();
            _autoFixture = new Fixture();
        }

        [Fact]
        public async void ShouldReturnOkWhenUpdatingCourse()
        {
            var existingCourse = _fixture.SchoolContext.Courses.First(c => c.Title == "test-title1");

            var existingStudents = _fixture.SchoolContext.Students.ToList();

            var students = new List<ApiModels.Student>
            {
                new ApiModels.Student {StudentId = existingStudents[0].StudentId},
                new ApiModels.Student {StudentId = existingStudents[1].StudentId}
            };

            var existingInstructors = _fixture.SchoolContext.Instructors.ToList();

            var instructors = new List<ApiModels.Instructor>
            {
                new ApiModels.Instructor {InstructorId = existingInstructors[0].InstructorId},
                new ApiModels.Instructor {InstructorId = existingInstructors[1].InstructorId}
            };

            var existingDepartments = _fixture.SchoolContext.Departments.ToList();

            var department = existingDepartments.First(d => d.Name == "test-name2");

            var courseId = existingCourse.CourseId;

            var course = _autoFixture.Build<ApiModels.Course>()
                .With(c => c.CourseId, courseId)
                .With(c => c.Title, "some-title")
                .With(c => c.Credits, 10)
                .With(c => c.Department, new ApiModels.Department { DepartmentId = department.DepartmentId})
                .With(c => c.Students, students)
                .With(c => c.Instructors, instructors)
                .Create();

            var studentJson = JsonConvert.SerializeObject(course);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PutAsync("api/courses", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseOfContent = await apiResponse.Content.ReadAsStringAsync();

            var responseOfCourse = JsonConvert.DeserializeObject<ApiModels.ApiResponse<ApiModels.Course>>(responseOfContent);

            responseOfCourse.IsSuccess.Should().BeTrue();
            responseOfCourse.Data.Title.Should().Be("some-title");
            responseOfCourse.Data.Credits.Should().Be(10);
            responseOfCourse.Data.Department.Name.Should().Be("test-name2");
            responseOfCourse.Data.Students.Count().Should().Be(2);
            responseOfCourse.Data.Instructors.Count().Should().Be(2);
        }
    }
}
