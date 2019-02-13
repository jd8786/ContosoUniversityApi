using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace ContosoUniversity.Api.Test.Validators
{
    [Trait("Category", "Unit Test: Api.Validators.Id")]
    public class IdValidatorTests
    {
        private readonly Mock<IStudentRepository> _studentRepository;
        private readonly Mock<ICourseRepository> _courseRepository;
        private readonly Mock<IDepartmentRepository> _departmentRepository;
        private readonly Mock<IInstructorRepository> _instructorRepository;
        private readonly IdValidator _idValidator;

        public IdValidatorTests()
        {
            _studentRepository = new Mock<IStudentRepository>();
            _courseRepository = new Mock<ICourseRepository>();
            _departmentRepository = new Mock<IDepartmentRepository>();
            _instructorRepository = new Mock<IInstructorRepository>();

            _idValidator = new IdValidator(_studentRepository.Object, _courseRepository.Object, _departmentRepository.Object, _instructorRepository.Object);

            _studentRepository.Setup(sr => sr.Find(1)).Returns(new StudentEntity { StudentId = 1 });

            _courseRepository.Setup(cr => cr.Find(1)).Returns(new CourseEntity { CourseId = 1 });

            _departmentRepository.Setup(dr => dr.Find(1)).Returns(new DepartmentEntity { DepartmentId = 1 });

            _instructorRepository.Setup(ir => ir.Find(1)).Returns(new InstructorEntity { InstructorId = 1 });
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhenStudentDoesNotExist()
        {
            var exception = Assert.Throws<NotFoundException>(() => _idValidator.ValidateStudentById(2));

            _studentRepository.Verify(sr => sr.Find(2), Times.Exactly(1));

            exception.Message.Should().Be("Student provided with Id 2 doesnot exist in the database");
        }

        [Fact]
        public void ShouldNotThrowNotFoundExceptionWhenStudentExists()
        {
            NotFoundException ex = null;

            try
            {
                _idValidator.ValidateStudentById(1);
            }
            catch (NotFoundException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
            _studentRepository.Verify(sr => sr.Find(1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhenCourseDoesNotExist()
        {
            var exception = Assert.Throws<NotFoundException>(() => _idValidator.ValidateCourseById(2));

            _courseRepository.Verify(cr => cr.Find(2), Times.Exactly(1));

            exception.Message.Should().Be("Course provided with Id 2 doesnot exist in the database");
        }

        [Fact]
        public void ShouldNotThrowNotFoundExceptionWhenCourseExists()
        {
            NotFoundException ex = null;

            try
            {
                _idValidator.ValidateCourseById(1);
            }
            catch (NotFoundException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
            _courseRepository.Verify(cr => cr.Find(1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhenDepartmentDoesNotExist()
        {
            var exception = Assert.Throws<NotFoundException>(() => _idValidator.ValidateDepartmentById(2));

            _departmentRepository.Verify(dr => dr.Find(2), Times.Exactly(1));

            exception.Message.Should().Be("Department provided with Id 2 doesnot exist in the database");
        }

        [Fact]
        public void ShouldNotThrowNotFoundExceptionWhenDepartmentExists()
        {
            NotFoundException ex = null;

            try
            {
                _idValidator.ValidateDepartmentById(1);
            }
            catch (NotFoundException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
            _departmentRepository.Verify(dr => dr.Find(1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionWhenInstructorDoesNotExist()
        {
            var exception = Assert.Throws<NotFoundException>(() => _idValidator.ValidateInstructorById(2));

            _instructorRepository.Verify(ir => ir.Find(2), Times.Exactly(1));

            exception.Message.Should().Be("Instructor provided with Id 2 doesnot exist in the database");
        }

        [Fact]
        public void ShouldNotThrowNotFoundExceptionWhenInstructorExists()
        {
            NotFoundException ex = null;

            try
            {
                _idValidator.ValidateInstructorById(1);
            }
            catch (NotFoundException e)
            {
                ex = e;
            }

            ex.Should().BeNull();
            _instructorRepository.Verify(ir => ir.Find(1), Times.Exactly(1));
        }
    }
}
