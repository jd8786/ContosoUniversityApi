using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.Exceptions;
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
        private readonly Mock<ICommonValidator> _commonValidator;

        public CourseValidatorTests()
        {
            _commonValidator = new Mock<ICommonValidator>();
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
        public void ShouldValidateChildrenWhenPostingCourseWithChildren()
        {
            var course = new Course
            {
                Students = new List<Student>
                {
                    new Student {StudentId = 1},
                    new Student {StudentId = 2}
                },
                Instructors = new List<Instructor>
                {
                    new Instructor {InstructorId = 1}
                },
                Department = new Department { DepartmentId = 1 }
            };

            _courseValidator.ValidatePostCourse(course);

            _commonValidator.Verify(cv => cv.ValidateStudentById(It.IsAny<int>()), Times.Exactly(2));
            _commonValidator.Verify(cv => cv.ValidateInstructorById(It.IsAny<int>()), Times.Exactly(1));
            _commonValidator.Verify(cv => cv.ValidateDepartmentById(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void ShouldNotValidateChildrenWhenPostingCourseWithNullChildren()
        {
            _courseValidator.ValidatePostCourse(new Course());

            _commonValidator.Verify(cv => cv.ValidateStudentById(It.IsAny<int>()), Times.Never);
            _commonValidator.Verify(cv => cv.ValidateInstructorById(It.IsAny<int>()), Times.Never);
            _commonValidator.Verify(cv => cv.ValidateDepartmentById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldNotValidateChildrenWhenPostingCourseWithEmptyChildren()
        {
            _courseValidator.ValidatePostCourse(
                new Course
                {
                    Students = new List<Student>(),
                    Instructors = new List<Instructor>()
                });

            _commonValidator.Verify(cv => cv.ValidateStudentById(It.IsAny<int>()), Times.Never);
            _commonValidator.Verify(cv => cv.ValidateInstructorById(It.IsAny<int>()), Times.Never);
            _commonValidator.Verify(cv => cv.ValidateDepartmentById(It.IsAny<int>()), Times.Never);
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
        public void ShouldCallValidateCourseById()
        {
            _courseValidator.ValidatePutCourse(new Course { CourseId = 1 });

            _commonValidator.Verify(cv => cv.ValidateCourseById(1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldValidateChildrenWhenPuttingCourseWithChildren()
        {
            var course = new Course
            {
                CourseId = 1,
                Students = new List<Student>
                {
                    new Student {StudentId = 1},
                    new Student {StudentId = 2}
                },
                Instructors = new List<Instructor>
                {
                    new Instructor {InstructorId = 1}
                },
                Department = new Department { DepartmentId = 1 }
            };

            _courseValidator.ValidatePutCourse(course);

            _commonValidator.Verify(cv => cv.ValidateStudentById(It.IsAny<int>()), Times.Exactly(2));
            _commonValidator.Verify(cv => cv.ValidateInstructorById(It.IsAny<int>()), Times.Exactly(1));
            _commonValidator.Verify(cv => cv.ValidateDepartmentById(It.IsAny<int>()), Times.Exactly(1));
        }

        [Fact]
        public void ShouldNotValidateChildrenWhenPuttingCourseWithNullChildren()
        {
            _courseValidator.ValidatePutCourse(new Course { CourseId = 1 });

            _commonValidator.Verify(cv => cv.ValidateStudentById(It.IsAny<int>()), Times.Never);
            _commonValidator.Verify(cv => cv.ValidateInstructorById(It.IsAny<int>()), Times.Never);
            _commonValidator.Verify(cv => cv.ValidateDepartmentById(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void ShouldNotValidateChildrenWhenPuttingCourseWithEmptyChildren()
        {
            _courseValidator.ValidatePutCourse(new Course
            {
                CourseId = 1,
                Students = new List<Student>(),
                Instructors = new List<Instructor>()
            });

            _commonValidator.Verify(cv => cv.ValidateStudentById(It.IsAny<int>()), Times.Never);
            _commonValidator.Verify(cv => cv.ValidateInstructorById(It.IsAny<int>()), Times.Never);
            _commonValidator.Verify(cv => cv.ValidateDepartmentById(It.IsAny<int>()), Times.Never);
        }
    }
}
