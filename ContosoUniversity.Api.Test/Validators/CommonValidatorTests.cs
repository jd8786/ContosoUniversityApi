using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.Exceptions;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ContosoUniversity.Api.Test.Validators
{
    [Trait("Category", "Unit Test: Api.Validators.Common")]
    public class CommonValidatorTests
    {
        private readonly Mock<IIdValidator> _idValidator;
        private readonly ICommonValidator _commonValidator;

        public CommonValidatorTests()
        {
            _idValidator = new Mock<IIdValidator>();
            _commonValidator = new CommonValidator(_idValidator.Object);
        }

        [Fact]
        public void ShouldThrowInvalidCourseExceptionWhenDepartmentOnCourseIsNull()
        {
            var exception = Assert.Throws<InvalidCourseException>(() => _commonValidator.ValidateCourseChildren(new Course()));

            exception.Message.Should().Be("Department must be provided");
        }

        [Fact]
        public void ShouldValidateDepartmentWhenDepartmentIsProvidedOnCourse()
        {
            _commonValidator.ValidateCourseChildren(new Course { Department = new Department() });

            _idValidator.Verify(iv => iv.ValidateDepartmentById(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void ShouldNotValidateStudentsWhenStudentsAreNullOnCourse()
        {
            _commonValidator.ValidateCourseChildren(new Course { Department = new Department() });

            _idValidator.Verify(iv => iv.ValidateStudentById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldNotValidateStudentsWhenStudentsAreEmptyOnCourse()
        {
            _commonValidator.ValidateCourseChildren(
                new Course
                {
                    Students = new List<Student>(),
                    Department = new Department()
                });

            _idValidator.Verify(iv => iv.ValidateStudentById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldValidateStudentsWhenStudentsAreNotNullOrEmptyOnCourse()
        {
            _commonValidator.ValidateCourseChildren(
                new Course
                {
                    Students = new List<Student> { new Student(), new Student() },
                    Department = new Department()
                });

            _idValidator.Verify(iv => iv.ValidateStudentById(It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public void ShouldNotValidateInstructorsWhenInstructorsAreNullOnCourse()
        {
            _commonValidator.ValidateCourseChildren(new Course {Department = new Department()});

            _idValidator.Verify(iv => iv.ValidateInstructorById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldNotValidateInstructorsWhenInstructorsAreEmptyOnCourse()
        {
            _commonValidator.ValidateCourseChildren(
                new Course
                {
                    Instructors = new List<Instructor>(),
                    Department = new Department()
                });

            _idValidator.Verify(iv => iv.ValidateInstructorById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldValidateInstructorsWhenInstructorsAreNotNullOrEmptyOnCourse()
        {
            _commonValidator.ValidateCourseChildren(
                new Course
                {
                    Instructors = new List<Instructor> { new Instructor(), new Instructor() },
                    Department = new Department()
                });

            _idValidator.Verify(iv => iv.ValidateInstructorById(It.IsAny<int>()), Times.Exactly(2));
        }

        [Fact]
        public void ShouldNotValidateCoursesWhenCoursesAreNullOnStudent()
        {
            _commonValidator.ValidateStudentChildren(new Student());

            _idValidator.Verify(iv => iv.ValidateCourseById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldNotValidateCoursesWhenCoursesAreEmptyOnStudent()
        {
            _commonValidator.ValidateStudentChildren(new Student { Courses = new List<Course>() });

            _idValidator.Verify(iv => iv.ValidateCourseById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldValidateCoursesWhenCoursesAreNotNullOrEmptyOnStudent()
        {
            _commonValidator.ValidateStudentChildren(new Student { Courses = new List<Course> { new Course(), new Course() } });

            _idValidator.Verify(iv => iv.ValidateCourseById(It.IsAny<int>()), Times.Exactly(2));
        }
    }
}
