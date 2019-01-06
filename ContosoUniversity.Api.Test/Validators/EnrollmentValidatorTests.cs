using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ContosoUniversity.Api.Test.Validators
{
    [Trait("Category", "Unit Test: Api.Validators.Enrollment")]
    public class EnrollmentValidatorTests
    {
        private readonly Mock<IStudentValidator> _studentValidator;

        private readonly Mock<ICourseValidator> _courseValidator;

        private readonly IEnrollmentValidator _enrollmentValidator;

        public EnrollmentValidatorTests()
        {
            _studentValidator = new Mock<IStudentValidator>();
            _courseValidator = new Mock<ICourseValidator>();
            _enrollmentValidator = new EnrollmentValidator(_studentValidator.Object, _courseValidator.Object);
        }

        [Fact]
        public void ShouldThrowInvalidEnrollmentExceptionWhenCallingValidatePostEnrollmentWithNullEnrollment()
        {
            var exception =
                Assert.Throws<InvalidEnrollmentException>(() => _enrollmentValidator.ValidatePostEnrollment(null));

            exception.Message.Should().Be("Enrollment must be provided");

            _studentValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);

            _courseValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldThrowInvalidEnrollmentExceptionWhenCallingValidatePostEnrollmentWithNonZeroEnrollmentId()
        {
            var exception =
                Assert.Throws<InvalidEnrollmentException>(() => _enrollmentValidator.ValidatePostEnrollment(new Enrollment { EnrollmentId = 1 }));

            exception.Message.Should().Be("Enrollment Id must be 0");

            _studentValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);

            _courseValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldThrowInvalidEnrollmentExceptionWhenCallingValidatePostEnrollmentWithZeroStudentId()
        {
            var exception =
                Assert.Throws<InvalidEnrollmentException>(() => _enrollmentValidator.ValidatePostEnrollment(new Enrollment { StudentId = 0 }));

            exception.Message.Should().Be("Student and course must be provided");

            _studentValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);

            _courseValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldThrowInvalidEnrollmentExceptionWhenCallingValidatePostEnrollmentWithZeroCourseId()
        {
            var exception =
                Assert.Throws<InvalidEnrollmentException>(() => _enrollmentValidator.ValidatePostEnrollment(new Enrollment { CourseId = 0 }));

            exception.Message.Should().Be("Student and course must be provided");

            _studentValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);

            _courseValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldNotThrowInvalidEnrollmentExceptionWhenCallingValidatePostEnrollmentWithValidEnrollment()
        {
            _enrollmentValidator.ValidatePostEnrollment(new Enrollment { StudentId = 1, CourseId = 1 });

            _studentValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Exactly(1));

            _courseValidator.Verify(c => c.Validate(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void ShouldThrowInvalidEnrollmentExceptionWhenCallingValidatePutEnrollmentWithNullEnrollment()
        {
            var exception =
                Assert.Throws<InvalidEnrollmentException>(() => _enrollmentValidator.ValidatePutEnrollment(null));

            exception.Message.Should().Be("Enrollment must be provided");

            _studentValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);

            _courseValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldThrowInvalidEnrollmentExceptionWhenCallingValidatePutEnrollmentWithZeroStudentId()
        {
            var exception =
                Assert.Throws<InvalidEnrollmentException>(() => _enrollmentValidator.ValidatePutEnrollment(new Enrollment { StudentId = 0 }));

            exception.Message.Should().Be("Student and course must be provided");

            _studentValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);

            _courseValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldThrowInvalidEnrollmentExceptionWhenCallingValidatePutEnrollmentWithZeroCourseId()
        {
            var exception =
                Assert.Throws<InvalidEnrollmentException>(() => _enrollmentValidator.ValidatePutEnrollment(new Enrollment { CourseId = 0 }));

            exception.Message.Should().Be("Student and course must be provided");

            _studentValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);

            _courseValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldNotThrowInvalidEnrollmentExceptionWhenCallingValidatePutEnrollmentWithValidEnrollment()
        {
            _enrollmentValidator.ValidatePutEnrollment(new Enrollment { StudentId = 1, CourseId = 1 });

            _studentValidator.Verify(s => s.Validate(It.IsAny<int>()), Times.Exactly(1));

            _courseValidator.Verify(c => c.Validate(It.IsAny<int>()), Times.Exactly(1));
        }
    }
}
