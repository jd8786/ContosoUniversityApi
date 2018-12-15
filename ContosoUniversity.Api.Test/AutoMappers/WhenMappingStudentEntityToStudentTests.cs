using AutoFixture;
using AutoMapper;
using ContosoUniversity.Api.AutoMappers;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using FluentAssertions;
using System;
using Xunit;

namespace ContosoUniversity.Api.Test.AutoMappers
{
    [Trait("Category", "Unit Test: Api.AutoMappers.Student")]
    public class WhenMappingStudentEntityToStudentTests
    {
        private readonly IMapper _mapper;

        private readonly Fixture _autoFixture = new Fixture();

        public WhenMappingStudentEntityToStudentTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new StudentEntityToStudentProfile());
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ShouldMapStudentEntityPropertiesToStudent()
        {
            var studentEntity = _autoFixture.Build<StudentEntity>()
                .Without(s => s.Enrollments)
                .Create();

            var student = _mapper.Map<Student>(studentEntity);

            student.StudentId.Should().Be(studentEntity.StudentId);
            student.LastName.Should().Be(studentEntity.LastName);
            student.FirstMidName.Should().Be(studentEntity.FirstMidName);
            student.OriginCountry.Should().Be(studentEntity.OriginCountry);
            student.EnrollmentDate.Should().Be(studentEntity.EnrollmentDate);
            student.Enrollments.Should().BeEmpty();
        }

        [Fact]
        public void ShouldMapStudentPropertiesToStudentEntity()
        {
            var student = _autoFixture.Build<Student>()
                .Without(s => s.Enrollments)
                .Create();

            var studentEntity = _mapper.Map<StudentEntity>(student);

            studentEntity.StudentId.Should().Be(student.StudentId);
            studentEntity.LastName.Should().Be(student.LastName);
            studentEntity.FirstMidName.Should().Be(student.FirstMidName);
            studentEntity.OriginCountry.Should().Be(student.OriginCountry);
            studentEntity.EnrollmentDate.Should().Be(student.EnrollmentDate);
            studentEntity.CreatedBy.Should().Be("ContosoUniversityUsers");
            studentEntity.CreatedDate.ToShortDateString().Should().Be(DateTime.Now.ToShortDateString());
            studentEntity.UpdatedBy.Should().BeNull();
            studentEntity.UpdatedDate.Should().BeNull();
            studentEntity.Enrollments.Should().BeEmpty();
        }
    }
}
