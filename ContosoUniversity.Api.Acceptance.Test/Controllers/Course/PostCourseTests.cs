using AutoFixture;
using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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

            var dbCourse = _fixture.SchoolContext.Courses.Include(c => c.Department).FirstOrDefault(c => c.Title == "some-title");

            dbCourse.Should().NotBeNull();
            dbCourse.Department.Name.Should().Be("test-name1");
        }

        [Fact]
        public async void ShouldReturnOkWhenPostingCourseWithChildren()
        {
            var existingDepartment = _fixture.SchoolContext.Departments.First(d => d.Name == "test-name2");

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

            var course = _autoFixture.Build<ApiModels.Course>()
                .With(c => c.CourseId, 0)
                .With(c => c.Title, "some-title")
                .With(c => c.Credits, 6)
                .With(c => c.Department, new ApiModels.Department { DepartmentId = existingDepartment.DepartmentId })
                .With(c => c.Instructors, instructors)
                .With(c => c.Students, students)
                .Create();

            var studentJson = JsonConvert.SerializeObject(course);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PostAsync("api/courses", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbCourse = _fixture.SchoolContext.Courses.Include(c => c.Enrollments).Include(c => c.CourseAssignments).Include(c => c.Department).FirstOrDefault(c => c.Title == "some-title");

            dbCourse.Should().NotBeNull();
            dbCourse.Department.Name.Should().Be("test-name2");
            dbCourse.CourseAssignments.Count.Should().Be(2);
            dbCourse.Enrollments.Count.Should().Be(2);
        }
    }
}
