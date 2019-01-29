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
        private readonly ICourseValidator _courseValidator;

        public CourseValidatorTests()
        {
            var courseRepository = new Mock<ICourseRepository>();
            _courseValidator = new CourseValidator(courseRepository.Object);

            courseRepository.Setup(cr => cr.GetAll())
                .Returns(new List<CourseEntity> { new CourseEntity { CourseId = 1 } });
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhenCourseDoesNotExist()
        {
            var exception = Assert.Throws<NotFoundException>(() => _courseValidator.ValidateById(2));

            exception.Message.Should().Be("Course provided with Id 2 doesnot exist in the database");
        }


        [Fact]
        public void ShouldNotThrowNotFoundExceptionWhenCourseExists()
        {
            NotFoundException ex = null;

            try
            {
                _courseValidator.ValidateById(1);
            }
            catch (NotFoundException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
        }

        [Fact]
        public void ShouldThrowInvalidCourseExceptionWhenPostingCourseWithNullCourse()
        {
            var exception = Assert.Throws<InvalidCourseException>(() => _courseValidator.ValidatePostCourse(null));

            exception.Message.Should().Be("Course must be provided");
        }

        [Fact]
        public void ShouldThrowInvalidCourseExceptionWhenPostingCourseWithNonZeroCourseId()
        {
            var exception = Assert.Throws<InvalidCourseException>(() => _courseValidator.ValidatePostCourse(new Course { CourseId = 1 }));

            exception.Message.Should().Be("Course Id must be 0");
        }


        [Fact]
        public void ShouldNotThrowInvalidCourseExceptionWhenPostingCourseWithValidCourse()
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
        public void ShouldThrowInvalidCourseExceptionWhenPuttingCourseWithNullCourse()
        {
            var exception = Assert.Throws<InvalidCourseException>(() => _courseValidator.ValidatePutCourse(null));

            exception.Message.Should().Be("Course must be provided");
        }

        [Fact]
        public void ShouldThrowInvalidCourseExceptionWhenPuttingCourseWithZeroCourseId()
        {
            var exception = Assert.Throws<InvalidCourseException>(() => _courseValidator.ValidatePutCourse(new Course()));

            exception.Message.Should().Be("Course Id cannot be 0");
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhenPuttingNonExistingCourse()
        {
            var exception = Assert.Throws<NotFoundException>(() => _courseValidator.ValidatePutCourse(new Course { CourseId = 2 }));

            exception.Message.Should().Be("Course provided with Id 2 doesnot exist in the database");
        }


        [Fact]
        public void ShouldNotThrowInvalidCourseExceptionWhenPuttingCourseWithValidCourse()
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
