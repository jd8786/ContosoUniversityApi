using AutoFixture;
using AutoMapper;
using ContosoUniversity.Api.AutoMappers;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Api.Test.AutoMappers
{
    [Trait("Category", "Unit Test: Api.AutoMappers.Student")]
    public class StudentProfileTests
    {
        private readonly IMapper _mapper;

        private readonly Fixture _fixture;

        public StudentProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new StudentProfile());
            });

            _mapper = new Mapper(config);

            _fixture = new Fixture();
        }

        [Fact]
        public void ShouldMapPrimaryPropertiesFromStudentEntityToStudent()
        {
            var studentEntity = _fixture.Build<StudentEntity>()
                .Without(s => s.Enrollments)
                .Create();

            var student = _mapper.Map<Student>(studentEntity);

            student.StudentId.Should().Be(studentEntity.StudentId);
            student.LastName.Should().Be(studentEntity.LastName);
            student.FirstMidName.Should().Be(studentEntity.FirstMidName);
            student.OriginCountry.Should().Be(studentEntity.OriginCountry);
            student.EnrollmentDate.Should().Be(studentEntity.EnrollmentDate);
            student.Courses.Should().BeEmpty();
        }

        [Fact]
        public void ShouldMapCoursesFromStudentEntityToStudent()
        {
            var courseEntity1 = new CourseEntity { Credits = 3, Title = "title1" };

            var courseEntity2 = new CourseEntity { Credits = 4, Title = "title2" };

            var enrollmentEntity1 = new EnrollmentEntity
            {
                CourseId = 1,
                Grade = Grade.B,
                Course = courseEntity1
            };

            var enrollmentEntity2 = new EnrollmentEntity
            {
                CourseId = 2,
                Grade = Grade.A,
                Course = courseEntity2
            };

            var studentEntity = new StudentEntity
            {
                Enrollments = new List<EnrollmentEntity> {enrollmentEntity1, enrollmentEntity2}
            };

            var student = _mapper.Map<Student>(studentEntity);

            student.Courses.Count().Should().Be(2);
            student.Courses.First().CourseId.Should().Be(1);
            student.Courses.First().Credits.Should().Be(3);
            student.Courses.First().Grade.Should().Be(Grade.B);
            student.Courses.First().Title.Should().Be("title1");
            student.Courses.Last().CourseId.Should().Be(2);
            student.Courses.Last().Credits.Should().Be(4);
            student.Courses.Last().Grade.Should().Be(Grade.A);
            student.Courses.Last().Title.Should().Be("title2");
        }

        [Fact]
        public void ShouldMapPrimaryPropertiesFromStudentToStudentEntity()
        {
            var student = _fixture.Build<Student>()
                .Without(s => s.Courses)
                .Create();

            var studentEntity = _mapper.Map<StudentEntity>(student);

            studentEntity.StudentId.Should().Be(student.StudentId);
            studentEntity.LastName.Should().Be(student.LastName);
            studentEntity.FirstMidName.Should().Be(student.FirstMidName);
            studentEntity.OriginCountry.Should().Be(student.OriginCountry);
            studentEntity.EnrollmentDate.Should().Be(student.EnrollmentDate);
            studentEntity.Enrollments.Should().BeEmpty();
        }

        [Fact]
        public void ShouldMapEnrollmentsFromStudentToStudentEntity()
        {
            var course1 = new Course { CourseId = 1, Title = "title1", Grade = Grade.A };
                                                     
            var course2 = new Course { CourseId = 2, Title = "title2", Grade = Grade.B};

            var student = new Student
            {
                StudentId = 1,
                Courses = new List<Course> { course1, course2 }
            };

            var studentEntity = _mapper.Map<StudentEntity>(student);

            studentEntity.Enrollments.Count.Should().Be(2);
            studentEntity.Enrollments.First().CourseId.Should().Be(1);
            studentEntity.Enrollments.First().StudentId.Should().Be(1);
            studentEntity.Enrollments.First().Grade.Should().Be(Grade.A);
            studentEntity.Enrollments.Last().CourseId.Should().Be(2);
            studentEntity.Enrollments.Last().StudentId.Should().Be(1);
            studentEntity.Enrollments.Last().Grade.Should().Be(Grade.B);
        }
    }
}
