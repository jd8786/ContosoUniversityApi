using System.Collections.Generic;
using AutoFixture;
using ContosoUniversity.Api.Acceptance.Test.Fixtures;
using ContosoUniversity.Api.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using AutoMapper;
using ContosoUniversity.Api.AutoMappers;
using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Students
{
    [Collection("Sequential")]
    [Trait("Category", "Acceptance Test: Update Student")]
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
        public async void ShouldReturnOkWhenUpdateStudentWithoutSelectedCourses()
        {
            var existingStudent = _fixture.SchoolContext.Students
                .Include(s => s.Enrollments)
                .First(s => s.LastName == "test-last-name1");

            existingStudent.Enrollments.Count.Should().Be(1);

            var studentId = existingStudent.StudentId;

            var student = _autoFixture.Build<Student>()
                .With(s => s.StudentId, studentId)
                .With(s => s.LastName, "some-last-name")
                .With(s => s.FirstMidName, "some-first-mid-name")
                .With(s => s.OriginCountry, "some-origin-country")
                .Without(s => s.Enrollments)
                .Create();

            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PutAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbContext = _fixture.SchoolContext;

            var dbStudent = dbContext.Students.Include(s => s.Enrollments).First(s => s.StudentId == studentId);

            dbStudent.LastName.Should().Be("some-last-name");
            dbStudent.FirstMidName.Should().Be("some-first-mid-name");
            dbStudent.OriginCountry.Should().Be("some-origin-country");
            dbStudent.Enrollments.Should().BeEmpty(); 
        }

        [Fact]
        public async void ShouldReturnOkWhenUpdateStudentWithTheSameSelectedCourses()
        {
            var existingStudent = _fixture.SchoolContext.Students
                .Include(s => s.Enrollments)
                .First(s => s.LastName == "test-last-name1");

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new EnrollmentEntityToEnrollmentProfile()));

            var mapper = mapperConfig.CreateMapper();

            var enrollments = mapper.Map<IEnumerable<Enrollment>>(existingStudent.Enrollments).ToList();

            enrollments.Count.Should().Be(1);

            var studentId = existingStudent.StudentId;

            var student = _autoFixture.Build<Student>()
                .With(s => s.StudentId, studentId)
                .With(s => s.LastName, "some-last-name")
                .With(s => s.FirstMidName, "some-first-mid-name")
                .With(s => s.OriginCountry, "some-origin-country")
                .With(s => s.EnrollmentDate, existingStudent.EnrollmentDate)
                .With(s => s.Enrollments, enrollments)
                .Create();

            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PutAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbContext = _fixture.SchoolContext;

            var dbStudent = dbContext.Students.Include(s => s.Enrollments).First(s => s.StudentId == studentId);

            dbStudent.LastName.Should().Be("some-last-name");
            dbStudent.FirstMidName.Should().Be("some-first-mid-name");
            dbStudent.OriginCountry.Should().Be("some-origin-country");
            dbStudent.Enrollments.Count(e => e.EnrollmentId == existingStudent.Enrollments.ToList()[0].EnrollmentId).Should().Be(1);
        }

        [Fact]
        public async void ShouldReturnOkWhenUpdateStudentWithTheSameSelectedCoursesButDifferentGrade()
        {
            var existingStudent = _fixture.SchoolContext.Students
                .Include(s => s.Enrollments)
                .First(s => s.LastName == "test-last-name1");

            existingStudent.Enrollments.First().Grade.Should().Be(Grade.A);

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new StudentEntityToStudentProfile()));

            var mapper = mapperConfig.CreateMapper();

            var student = mapper.Map<Student>(existingStudent);

            student.Enrollments.First().Grade = Grade.B;

            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PutAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbContext = _fixture.SchoolContext;

            var studentId = existingStudent.StudentId;

            var dbStudent = dbContext.Students.Include(s => s.Enrollments).First(s => s.StudentId == studentId);

            dbStudent.Enrollments.First(e => e.EnrollmentId == existingStudent.Enrollments.First().EnrollmentId).Grade.Should().Be(Grade.B);
        }

        [Fact]
        public async void ShouldReturnOkWhenUpdateStudentWithDifferentSelectedCourses()
        {
            var existingStudent = _fixture.SchoolContext.Students
                .Include(s => s.Enrollments)
                .First(s => s.LastName == "test-last-name1");

            existingStudent.Enrollments.First().CourseId.Should().Be(1050);

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new StudentEntityToStudentProfile()));

            var mapper = mapperConfig.CreateMapper();

            var student = mapper.Map<Student>(existingStudent);

            student.Enrollments.First().CourseId = 4022;

            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PutAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbContext = _fixture.SchoolContext;

            var studentId = existingStudent.StudentId;

            var dbStudent = dbContext.Students.Include(s => s.Enrollments).First(s => s.StudentId == studentId);

            dbStudent.Enrollments.First(e => e.EnrollmentId == existingStudent.Enrollments.First().EnrollmentId)
                .CourseId.Should().Be(4022);
        }

        [Fact]
        public async void ShouldReturnOkWhenUpdateStudentWithNewSelectedCourses()
        {
            var existingStudent = _fixture.SchoolContext.Students
                .Include(s => s.Enrollments)
                .First(s => s.LastName == "test-last-name1");

            existingStudent.Enrollments.Count.Should().Be(1);

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new StudentEntityToStudentProfile()));

            var mapper = mapperConfig.CreateMapper();

            var student = mapper.Map<Student>(existingStudent);

            student.Enrollments = new List<Enrollment>
            {
                new Enrollment {StudentId = existingStudent.StudentId, CourseId = 4022},
                mapper.Map<Enrollment>(existingStudent.Enrollments.First())
            };
        
            var studentJson = JsonConvert.SerializeObject(student);

            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

            var apiResponse = await _fixture.HttpClient.PutAsync("api/students", content);

            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var dbContext = _fixture.SchoolContext;

            var studentId = existingStudent.StudentId;

            var dbStudent = dbContext.Students.Include(s => s.Enrollments).First(s => s.StudentId == studentId);

            dbStudent.Enrollments.Count.Should().Be(2);
        }
    }
}
