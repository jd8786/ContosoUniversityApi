using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ContosoUniversity.Api.Test.Validators
{
    [Trait("Category", "Unit Test: Api.Validators.Course")]
    public class CourseValidatorTests
    {
        private readonly Mock<ICourseRepository> _courseRepository;

        private readonly ICourseValidator _courseValidator;

        public CourseValidatorTests()
        {
            _courseRepository = new Mock<ICourseRepository>();
            _courseValidator = new CourseValidator(_courseRepository.Object);
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhenCourseDoesNotExist()
        {
            _courseRepository.Setup(c => c.GetAll()).Returns(new List<CourseEntity> { new CourseEntity { CourseId = 1 } });

            var exception = Assert.Throws<NotFoundException>(() => _courseValidator.Validate(2));

            exception.Message.Should().Be("Course provided with Id 2 doesnot exist in the database");
        }


        [Fact]
        public void ShouldNotThrowNotFoundExceptionWhenCourseExists()
        {
            NotFoundException ex = null;

            _courseRepository.Setup(c => c.GetAll()).Returns(new List<CourseEntity> { new CourseEntity { CourseId = 1 } });

            try
            {
                _courseValidator.Validate(1);
            }
            catch (NotFoundException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
        }

        [Fact]
        public void ShouldThrowInvalidCourseExceptionWhenCallingValidatePostCourseWithNullCourse()
        {
            var exception = Assert.Throws<InvalidCourseException>(() => _courseValidator.ValidatePostCourse(null));

            exception.Message.Should().Be("Course must be provided");
        }

        [Fact]
        public void ShouldThrowInvalidCourseExceptionWhenCallingValidatePostCourseWithNonZeroCourseId()
        {
            var exception = Assert.Throws<InvalidCourseException>(() => _courseValidator.ValidatePostCourse(new Course { CourseId = 1 }));

            exception.Message.Should().Be("Course Id must be 0");
        }


        [Fact]
        public void ShouldNotThrowInvalidCourseExceptionWhenCallingValidatePostCourseWithValidCourse()
        {
            InvalidCourseException ex = null;

            try
            {
                _courseValidator.ValidatePostCourse(new Course());
            }
            catch (InvalidCourseException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
        }

        [Fact]
        public void ShouldThrowInvalidCourseExceptionWhenCallingValidatePutCourseWithNullCourse()
        {
            var exception = Assert.Throws<InvalidCourseException>(() => _courseValidator.ValidatePutCourse(null));

            exception.Message.Should().Be("Course must be provided");
        }

        [Fact]
        public void ShouldThrowInvalidCourseExceptionWhenCallingValidatePutCourseWithZeroCourseId()
        {
            var exception = Assert.Throws<InvalidCourseException>(() => _courseValidator.ValidatePutCourse(new Course()));

            exception.Message.Should().Be("Course Id cannot be 0");
        }


        [Fact]
        public void ShouldNotThrowInvalidCourseExceptionWhenCallingValidatePutCourseWithValidCourse()
        {
            InvalidCourseException ex = null;

            try
            {
                _courseValidator.ValidatePutCourse(new Course { CourseId = 1 });
            }
            catch (InvalidCourseException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
        }
    }
}
