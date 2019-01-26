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
        private readonly Mock<IStudentRepository> _studentRepository;

        private readonly IStudentValidator _studentValidator;

        public StudentValidatorTests()
        {
            _studentRepository = new Mock<IStudentRepository>();
            _studentValidator = new StudentValidator(_studentRepository.Object);
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhenStudentDoesNotExist()
        {
            _studentRepository.Setup(c => c.GetAll()).Returns(new List<StudentEntity> { new StudentEntity { StudentId = 1 } });

            var exception = Assert.Throws<NotFoundException>(() => _studentValidator.Validate(2));

            exception.Message.Should().Be("Student provided with Id 2 doesnot exist in the database");
        }


        [Fact]
        public void ShouldNotThrowNotFoundExceptionWhenStudentExists()
        {
            NotFoundException ex = null;

            _studentRepository.Setup(c => c.GetAll()).Returns(new List<StudentEntity> { new StudentEntity() { StudentId = 1 } });

            try
            {
                _studentValidator.Validate(1);
            }
            catch (NotFoundException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
        }

        [Fact]
        public void ShouldThrowInvalidStudentExceptionWhenCallingValidatePostStudentWithNullStudent()
        {
            var exception = Assert.Throws<InvalidStudentException>(() => _studentValidator.ValidatePostStudent(null));

            exception.Message.Should().Be("Student must be provided");
        }

        [Fact]
        public void ShouldThrowInvalidStudentExceptionWhenCallingValidatePostStudentWithNonZeroStudentId()
        {
            var exception = Assert.Throws<InvalidStudentException>(() => _studentValidator.ValidatePostStudent(new Student() { StudentId = 1 }));

            exception.Message.Should().Be("Student Id must be 0");
        }


        [Fact]
        public void ShouldNotThrowInvalidStudentExceptionWhenCallingValidatePostStudentWithValidStudent()
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
        public void ShouldThrowInvalidStudentExceptionWhenCallingValidatePutStudentWithNullStudent()
        {
            var exception = Assert.Throws<InvalidStudentException>(() => _studentValidator.ValidatePutStudent(null));

            exception.Message.Should().Be("Student must be provided");
        }

        [Fact]
        public void ShouldThrowInvalidStudentExceptionWhenCallingValidatePutStudentWithZeroStudentId()
        {
            var exception = Assert.Throws<InvalidStudentException>(() => _studentValidator.ValidatePutStudent(new Student()));

            exception.Message.Should().Be("Student Id cannot be 0");
        }


        [Fact]
        public void ShouldNotThrowInvalidStudentExceptionWhenCallingValidatePutStudentWithValidStudent()
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
