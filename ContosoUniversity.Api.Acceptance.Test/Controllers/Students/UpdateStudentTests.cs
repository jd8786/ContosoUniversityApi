//using System;
//using System.Collections.Generic;
//using AutoFixture;
//using ContosoUniversity.Api.Acceptance.Test.Fixtures;
//using ContosoUniversity.Api.Models;
//using FluentAssertions;
//using Newtonsoft.Json;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using AutoMapper;
//using ContosoUniversity.Api.AutoMappers;
//using ContosoUniversity.Data.EntityModels;
//using Microsoft.EntityFrameworkCore;
//using Xunit;

//namespace ContosoUniversity.Api.Acceptance.Test.Controllers.Students
//{
//    [Collection("Sequential")]
//    [Trait("Category", "Acceptance Test: Api.Controllers.Students.UpdateStudent")]
//    public class UpdateStudentTests
//    {
//        private readonly AcceptanceTestFixture _fixture;

//        private readonly Fixture _autoFixture;

//        public UpdateStudentTests(AcceptanceTestFixture fixture)
//        {
//            _fixture = fixture;
//            _fixture.ResetDatabase();
//            _autoFixture = new Fixture();
//        }

//        [Fact]
//        public async void ShouldReturnOkWhenOnlyUpdateStudentProperties()
//        {
//            var existingStudent = _fixture.SchoolContext.Students
//                .Include(s => s.Enrollments)
//                .First(s => s.LastName == "test-last-name1");

//            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new EnrollmentEntityToStudentCourseProfile()));

//            var mapper = mapperConfig.CreateMapper();

//            var enrollments = mapper.Map<IEnumerable<Enrollment>>(existingStudent.Enrollments).ToList();

//            var studentId = existingStudent.StudentId;

//            var student = _autoFixture.Build<Student>()
//                .With(s => s.StudentId, studentId)
//                .With(s => s.LastName, "some-last-name")
//                .With(s => s.FirstMidName, "some-first-mid-name")
//                .With(s => s.OriginCountry, "some-origin-country")
//                .With(s => s.EnrollmentDate, new DateTime(2018, 1, 1))
//                .With(s => s.Courses, enrollments)
//                .Create();

//            var studentJson = JsonConvert.SerializeObject(student);

//            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

//            var apiResponse = await _fixture.HttpClient.PutAsync("api/students", content);

//            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

//            var dbContext = _fixture.SchoolContext;

//            var dbStudent = dbContext.Students.Include(s => s.Enrollments).First(s => s.StudentId == studentId);

//            dbStudent.LastName.Should().Be("some-last-name");
//            dbStudent.FirstMidName.Should().Be("some-first-mid-name");
//            dbStudent.OriginCountry.Should().Be("some-origin-country");
//            dbStudent.EnrollmentDate.Should().Be(new DateTime(2018, 1, 1));
//        }

//        [Fact]
//        public async void ShouldReturnOkWhenOnlyUpdateStudentEnrollments()
//        {
//            var existingStudent = _fixture.SchoolContext.Students
//                .Include(s => s.Enrollments)
//                .First(s => s.LastName == "test-last-name1");

//            existingStudent.Enrollments.First().CourseId.Should().Be(1050);

//            var studentId = existingStudent.StudentId;

//            var student = _autoFixture.Build<Student>()
//                .With(s => s.StudentId, studentId)
//                .With(s => s.LastName, existingStudent.LastName)
//                .With(s => s.FirstMidName, existingStudent.FirstMidName)
//                .With(s => s.OriginCountry, existingStudent.OriginCountry)
//                .With(s => s.EnrollmentDate, existingStudent.EnrollmentDate)
//                .With(s => s.Courses, new List<Enrollment> { new Enrollment { StudentId = studentId, CourseId = 4022 } })
//                .Create();

//            var studentJson = JsonConvert.SerializeObject(student);

//            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

//            var apiResponse = await _fixture.HttpClient.PutAsync("api/students", content);

//            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

//            var dbContext = _fixture.SchoolContext;

//            var dbStudent = dbContext.Students.Include(s => s.Enrollments).First(s => s.StudentId == studentId);

//            dbStudent.Enrollments.First().CourseId.Should().Be(4022);
//            dbStudent.Enrollments.Any(e => e.CourseId == 1050).Should().BeFalse();
//        }

//        [Fact]
//        public async void ShouldReturnOkWhenUpdateStudentPropertiesAndEnrollments()
//        {
//            var existingStudent = _fixture.SchoolContext.Students
//                .Include(s => s.Enrollments)
//                .First(s => s.LastName == "test-last-name1");

//            existingStudent.Enrollments.First().CourseId.Should().Be(1050);

//            var studentId = existingStudent.StudentId;

//            var student = _autoFixture.Build<Student>()
//                .With(s => s.StudentId, studentId)
//                .With(s => s.LastName, "some-last-name")
//                .With(s => s.FirstMidName, "some-first-mid-name")
//                .With(s => s.OriginCountry, "some-origin-country")
//                .With(s => s.EnrollmentDate, new DateTime(2018, 1, 1))
//                .Without(s => s.Courses)
//                .Create();

//            var studentJson = JsonConvert.SerializeObject(student);

//            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

//            var apiResponse = await _fixture.HttpClient.PutAsync("api/students", content);

//            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

//            var dbContext = _fixture.SchoolContext;

//            var dbStudent = dbContext.Students.Include(s => s.Enrollments).First(s => s.StudentId == studentId);

//            dbStudent.LastName.Should().Be("some-last-name");
//            dbStudent.FirstMidName.Should().Be("some-first-mid-name");
//            dbStudent.OriginCountry.Should().Be("some-origin-country");
//            dbStudent.EnrollmentDate.Should().Be(new DateTime(2018, 1, 1));
//            dbStudent.Enrollments.Should().BeEmpty();
//        }

//        [Fact]
//        public async void ShouldReturnOkWhenUpdateStudentEnrollmentGrade()
//        {
//            var existingStudent = _fixture.SchoolContext.Students
//                .Include(s => s.Enrollments)
//                .First(s => s.LastName == "test-last-name1");

//            existingStudent.Enrollments.First().Grade.Should().Be(Grade.A);

//            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new StudentEntityToStudentProfile()));

//            var mapper = mapperConfig.CreateMapper();

//            var student = mapper.Map<Student>(existingStudent);

//            student.Courses.First().Grade = Grade.B;

//            var studentJson = JsonConvert.SerializeObject(student);

//            var content = new StringContent(studentJson, Encoding.UTF8, "application/json");

//            var apiResponse = await _fixture.HttpClient.PutAsync("api/students", content);

//            apiResponse.StatusCode.Should().Be(HttpStatusCode.OK);

//            var dbContext = _fixture.SchoolContext;

//            var studentId = existingStudent.StudentId;

//            var dbStudent = dbContext.Students.Include(s => s.Enrollments).First(s => s.StudentId == studentId);

//            dbStudent.Enrollments.First().Grade.Should().Be(Grade.B);
//        }
//    }
//}
