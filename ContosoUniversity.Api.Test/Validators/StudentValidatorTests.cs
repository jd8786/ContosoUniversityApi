using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ContosoUniversity.Api.Test.Validators
{
    [Trait("Category", "Unit Test: Api.Validators.Student")]
    public class StudentValidatorTests
    {
        private readonly IStudentValidator _studentValidator;
        private readonly Mock<ICommonValidator> _commonValidator;
        private readonly Mock<IIdValidator> _idValidator;

        public StudentValidatorTests()
        {
            _commonValidator = new Mock<ICommonValidator>();

            _idValidator = new Mock<IIdValidator>();

            _commonValidator.Setup(cv => cv.IdValidator).Returns(_idValidator.Object);

            _studentValidator = new StudentValidator(_commonValidator.Object);
        }

        [Fact]
        public void ShouldThrowInvalidStudentExceptionWhenPostingStudentWithNullStudent()
        {
            var exception = Assert.Throws<InvalidStudentException>(() => _studentValidator.ValidatePostStudent(null));

            exception.Message.Should().Be("Student must be provided");
        }

        [Fact]
        public void ShouldThrowInvalidStudentExceptionWhenPostingStudentWithNonZeroStudentId()
        {
            var exception = Assert.Throws<InvalidStudentException>(() => _studentValidator.ValidatePostStudent(new Student { StudentId = 1 }));

            exception.Message.Should().Be("Student Id must be 0");
        }

        [Fact]
        public void ShouldValidateChildrenWhenPostingStudent()
        {
            var student = new Student();

            _studentValidator.ValidatePostStudent(student);

            _commonValidator.Verify(cv => cv.ValidateStudentChildren(student), Times.Exactly(1));
        }

        [Fact]
        public void ShouldThrowInvalidStudentExceptionWhenPuttingStudentWithNullStudent()
        {
            var exception = Assert.Throws<InvalidStudentException>(() => _studentValidator.ValidatePutStudent(null));

            exception.Message.Should().Be("Student must be provided");
        }

        [Fact]
        public void ShouldThrowInvalidStudentExceptionWhenPuttingStudentWithZeroStudentId()
        {
            var exception = Assert.Throws<InvalidStudentException>(() => _studentValidator.ValidatePutStudent(new Student()));

            exception.Message.Should().Be("Student Id cannot be 0");
        }

        [Fact]
        public void ShouldCallValidateStudentByIdWhenPuttingStudent()
        {
            var student = new Student { StudentId = 1 };

            _studentValidator.ValidatePutStudent(student);

            _idValidator.Verify(iv => iv.ValidateStudentById(1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldValidateChildrenWhenPuttingStudent()
        {
            var student = new Student { StudentId = 1 };

            _studentValidator.ValidatePutStudent(student);

            _commonValidator.Verify(cv => cv.ValidateStudentChildren(student), Times.Exactly(1));
        }
    }
}
