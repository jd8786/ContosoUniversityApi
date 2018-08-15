using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.Models;
using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ContosoUniversity.Data.Tests.Repositories
{
    [Trait("Category", "Unit Test: Data.Repositories.Students")]
    public class StudentRepositoryTests
    {
        private List<Student> _students;

        private readonly IStudentsRepository _repository;

        public StudentRepositoryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();

            var mockDbContext = new Mock<SchoolContext>(optionsBuilder.Options);

            var mockSet = new Mock<DbSet<Student>>();

            _students = new List<Student>
            {
                new Student { StudentId = 1, LastName = "some-last-name1"},
                new Student { StudentId = 2, LastName = "some-last-name2" },
                new Student { StudentId = 3, LastName = "some-last-name3" }
            }; ;

            var queryableStudents = _students.AsQueryable();

            mockSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(queryableStudents.Provider);
            mockSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(queryableStudents.Expression);
            mockSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(queryableStudents.ElementType);
            mockSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(queryableStudents.GetEnumerator());

            mockDbContext.Setup(c => c.Students).Returns(mockSet.Object);

            _repository = new StudentsRepository(mockDbContext.Object);
        }

        [Fact]
        public void ShouldReturnAllTheStudents()
        {
            var students = _repository.GetStudents();

            students.Count().Should().Be(3);
        }

        [Fact]
        public void ShouldReturnStudentWhenIdExists()
        {
            var student = _repository.GetStudentById(2);

            student.Should().NotBeNull();

            student.LastName.Should().Be("some-last-name2");
        }

        [Fact]
        public void ShouldReturnNullWhenIdDoesNotExist()
        {
            var student = _repository.GetStudentById(4);

            student.Should().BeNull();
        }
    }
}
