using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ContosoUniversity.Api.Test.Validators
{
    [Trait("Category", "Unit Test: Api.Validators.Course")]
    public class CourseValidatorTests
    {
        private readonly ICourseValidator _courseValidator;
        private readonly Mock<ICommonValidator> _commonValidator;
        private readonly Mock<IIdValidator> _idValidator;

        public CourseValidatorTests()
        {
            _commonValidator = new Mock<ICommonValidator>();

            _idValidator = new Mock<IIdValidator>();

            _commonValidator.Setup(cv => cv.IdValidator).Returns(_idValidator.Object);

            _courseValidator = new CourseValidator(_commonValidator.Object);
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
        public void ShouldValidateChildrenWhenPostingCourse()
        {
            var course = new Course();

            _courseValidator.ValidatePostCourse(course);

            _commonValidator.Verify(cv => cv.ValidateCourseChildren(course), Times.Exactly(1));
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
        public void ShouldCallValidateCourseByIdWhenPuttingCourse()
        {
            _courseValidator.ValidatePutCourse(new Course { CourseId = 1 });

            _idValidator.Verify(iv => iv.ValidateCourseById(1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldValidateChildrenWhenPuttingCourse()
        {
            var course = new Course { CourseId = 1 };

            _courseValidator.ValidatePutCourse(course);

            _commonValidator.Verify(cv => cv.ValidateCourseChildren(course), Times.Exactly(1));
        }
    }
}
