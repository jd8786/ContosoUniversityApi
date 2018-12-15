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
    [Trait("Category", "Unit Test: Api.AutoMappers.Enrollment")]
    public class WhenMappingEnrollmentEntityToEnrollmentTests
    {
        private readonly IMapper _mapper;

        private readonly Fixture _autoFixture = new Fixture();

        public WhenMappingEnrollmentEntityToEnrollmentTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EnrollmentEntityToEnrollmentProfile());
            });

            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ShouldMapEnrollmentEntityPropertiesToEnrollment()
        {
            var enrollmentEntity = _autoFixture.Build<EnrollmentEntity>()
                .Without(e => e.Student)
                .Without(e => e.Course)
                .Create();

            var enrollment = _mapper.Map<Enrollment>(enrollmentEntity);

            enrollment.EnrollmentId.Should().Be(enrollmentEntity.EnrollmentId);
            enrollment.StudentId.Should().Be(enrollmentEntity.StudentId);
            enrollment.CourseId.Should().Be(enrollmentEntity.CourseId);
            enrollment.Grade.Should().Be(enrollmentEntity.Grade);
        }

        [Fact]
        public void ShouldMapEnrollmentPropertiesToEnrollmentEntity()
        {
            var enrollment = _autoFixture.Build<Enrollment>()
                .Create();

            var enrollmentEntity = _mapper.Map<EnrollmentEntity>(enrollment);

            enrollmentEntity.EnrollmentId.Should().Be(enrollment.EnrollmentId);
            enrollmentEntity.StudentId.Should().Be(enrollment.StudentId);
            enrollmentEntity.CourseId.Should().Be(enrollment.CourseId);
            enrollmentEntity.Grade.Should().Be(enrollment.Grade);
            enrollmentEntity.Student.Should().BeNull();
            enrollmentEntity.Course.Should().BeNull();
        }
    }
}
