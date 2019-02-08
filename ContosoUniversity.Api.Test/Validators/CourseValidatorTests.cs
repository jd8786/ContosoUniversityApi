using System;
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
        private readonly Mock<ICommonValidator> _commonValidator;

        public CourseValidatorTests()
        {
            var courseRepository = new Mock<ICourseRepository>();
            _commonValidator = new Mock<ICommonValidator>();
            _courseValidator = new CourseValidator(_commonValidator.Object);

            courseRepository.Setup(cr => cr.GetAll())
                .Returns(new List<CourseEntity> { new CourseEntity { CourseId = 1 } });
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
        public void ShouldThrowNotFoundExceptionWhenPuttingNonExistingCourse()
        {
            var exception = Assert.Throws<NotFoundException>(() => _courseValidator.ValidatePutCourse(new Course { CourseId = 2 }));

            exception.Message.Should().Be("Course provided with Id 2 doesnot exist in the database");
        }

        [Fact]
        public void ShouldValidateChildrenWhenPuttingCourse()
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
        public void ShouldNotThrowExceptionWhenPuttingCourseWithValidCourse()
        {
            Exception ex = null;

            try
            {
                _courseValidator.ValidatePutCourse(new Course {CourseId = 1});
            }
            catch (InvalidCourseException e)
            {
                ex = e;
            }
            catch (NotFoundException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
            _commonValidator.Verify(cv => cv.ValidateStudentById(It.IsAny<int>()), Times.Never);
            _commonValidator.Verify(cv => cv.ValidateInstructorById(It.IsAny<int>()), Times.Never);
            _commonValidator.Verify(cv => cv.ValidateDepartmentById(It.IsAny<int>()), Times.Never);
        }
    }
}
