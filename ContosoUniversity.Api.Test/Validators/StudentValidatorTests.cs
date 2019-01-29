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
    [Trait("Category", "Unit Test: Api.Validators.Student")]
    public class StudentValidatorTests
    {
        private readonly IStudentValidator _studentValidator;

        public StudentValidatorTests()
        {
            var studentRepository = new Mock<IStudentRepository>();
            _studentValidator = new StudentValidator(studentRepository.Object);

            studentRepository.Setup(c => c.GetAll()).Returns(new List<StudentEntity> { new StudentEntity { StudentId = 1 } });
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhenStudentDoesNotExist()
        {
            var exception = Assert.Throws<NotFoundException>(() => _studentValidator.ValidateById(2));

            exception.Message.Should().Be("Student provided with Id 2 doesnot exist in the database");
        }


        [Fact]
        public void ShouldNotThrowNotFoundExceptionWhenStudentExists()
        {
            NotFoundException ex = null;

            try
            {
                _studentValidator.ValidateById(1);
            }
            catch (NotFoundException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
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
        public void ShouldNotThrowInvalidStudentExceptionWhenPostingStudentWithValidStudent()
        {
            InvalidStudentException ex = null;

            try
            {
                _studentValidator.ValidatePostStudent(new Student());
            }
            catch (InvalidStudentException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
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
        public void ShouldThrowNotFoundExceptionWhenPuttingNonExistingStudent()
        {
            var exception = Assert.Throws<NotFoundException>(() => _studentValidator.ValidatePutStudent(new Student { StudentId = 2 }));

            exception.Message.Should().Be("Student provided with Id 2 doesnot exist in the database");
        }

        [Fact]
        public void ShouldNotThrowInvalidStudentExceptionWhenPuttingStudentWithValidStudent()
        {
            InvalidStudentException ex = null;

            try
            {
                _studentValidator.ValidatePutStudent(new Student { StudentId = 1 });
            }
            catch (InvalidStudentException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
        }
    }
}
