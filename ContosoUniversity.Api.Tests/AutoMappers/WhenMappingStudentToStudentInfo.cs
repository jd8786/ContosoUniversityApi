using System.Collections.Generic;
using AutoFixture;
using AutoMapper;
using ContosoUniversity.Api.AutoMappers;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using FluentAssertions;
using Xunit;

namespace ContosoUniversity.Api.Tests.AutoMappers
{
    [Trait("Category", "Unit Test: Api.AutoMappers")]
    public class WhenMappingStudentToStudentInfo
    {
        private readonly IMapper _mapper;

        private readonly Fixture _autoFixture = new Fixture();

        public WhenMappingStudentToStudentInfo()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new StudentEntityToStudentProfile());
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ShouldMapStudentToStudentInfo()
        {
            var course1 = new CourseEntity {CourseId = 1001, Title = "some-title1", Credits = 3};

            var course2 = new CourseEntity { CourseId = 1002, Title = "some-title2", Credits = 4 };

            var enrollment1 = new EnrollmentEntity {Course = course1, StudentId = 1, Grade = Grade.A};

            var enrollment2 = new EnrollmentEntity {Course = course2, StudentId = 1, Grade = Grade.B};

            var student = _autoFixture.Build<StudentEntity>()
                .With(s => s.StudentId, 1)
                .With(s => s.Enrollments,
                    new List<EnrollmentEntity> { enrollment1, enrollment2 })
                .Create();

            var studentInfo = _mapper.Map<StudentInfo>(student);

            studentInfo.StudentInfoId.Should().Be(1);
            studentInfo.LastName.Should().Be(student.LastName);
            studentInfo.FirstMidName.Should().Be(student.FirstMidName);
            studentInfo.EnrollmentDate.Should().Be(student.EnrollmentDate);
            studentInfo.CourseInfos.Count.Should().Be(2);

            var courseInfos = studentInfo.CourseInfos;

            courseInfos[0].CourseInfoId.Should().Be(1001);
            courseInfos[0].Title.Should().Be("some-title1");
            courseInfos[0].Credits.Should().Be(3);
            courseInfos[0].Grade.Should().Be(Grade.A);

            courseInfos[1].CourseInfoId.Should().Be(1002);
            courseInfos[1].Title.Should().Be("some-title2");
            courseInfos[1].Credits.Should().Be(4);
            courseInfos[1].Grade.Should().Be(Grade.B);
        }
    }
}
