using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.Exceptions;
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
        private readonly Mock<ICommonValidator> _commonValidator;

        public StudentValidatorTests()
        {
            _commonValidator = new Mock<ICommonValidator>();

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
        public void ShouldValidateChildrenWhenPostingStudentWithCourses()
        {
            var student = new Student
            {
                Courses = new List<Course>
                {
                    new Course {CourseId = 1},
                    new Course {CourseId = 2}
                }
            };

            _studentValidator.ValidatePostStudent(student);

            _commonValidator.Verify(cv => cv.ValidateCourseById(It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public void ShouldNotValidateChildrenWhenPostingStudentWithNullCourses()
        {
            _studentValidator.ValidatePostStudent(new Student());

            _commonValidator.Verify(cv => cv.ValidateCourseById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldNotValidateChildrenWhenPostingStudentWithEmptyCourses()
        {
            _studentValidator.ValidatePostStudent(new Student { Courses = new List<Course>() });

            _commonValidator.Verify(cv => cv.ValidateCourseById(It.IsAny<int>()), Times.Never);
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
        public void ShouldCallValidateStudentById()
        {
            var student = new Student
            {
                StudentId = 1
            };

            _studentValidator.ValidatePutStudent(student);

            _commonValidator.Verify(cv => cv.ValidateStudentById(1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldValidateChildrenWhenPuttingStudentWithCourses()
        {
            var student = new Student
            {
                StudentId = 1,
                Courses = new List<Course>
                {
                    new Course {CourseId = 1},
                    new Course {CourseId = 2}
                }
            };

            _studentValidator.ValidatePutStudent(student);

            _commonValidator.Verify(cv => cv.ValidateCourseById(It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public void ShouldNotValidateChildrenWhenPuttingStudentWithNullCourses()
        {
            _studentValidator.ValidatePutStudent(new Student { StudentId = 1 });

            _commonValidator.Verify(cv => cv.ValidateCourseById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldNotValidateChildrenWhenPuttingStudentWithEmptyCourses()
        {
            _studentValidator.ValidatePutStudent(new Student { StudentId = 1, Courses = new List<Course>() });

            _commonValidator.Verify(cv => cv.ValidateCourseById(It.IsAny<int>()), Times.Never);
        }
    }
}
