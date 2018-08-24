using System;
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
        private readonly IStudentsRepository _repository;

        private readonly Mock<SchoolContext> _mockDbContext;

        private readonly Mock<DbSet<Student>> _mockDbSet;

        public StudentRepositoryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();

            _mockDbContext = new Mock<SchoolContext>(optionsBuilder.Options);

            _mockDbSet = new Mock<DbSet<Student>>();

            var students = new List<Student>
            {
                new Student { StudentId = 1, LastName = "some-last-name1"},
                new Student { StudentId = 2, LastName = "some-last-name2" },
                new Student { StudentId = 3, LastName = "some-last-name3" }
            }; ;

            var queryableStudents = students.AsQueryable();

            _mockDbSet.As<IQueryable<Student>>().Setup(m => m.Provider).Returns(queryableStudents.Provider);
            _mockDbSet.As<IQueryable<Student>>().Setup(m => m.Expression).Returns(queryableStudents.Expression);
            _mockDbSet.As<IQueryable<Student>>().Setup(m => m.ElementType).Returns(queryableStudents.ElementType);
            _mockDbSet.As<IQueryable<Student>>().Setup(m => m.GetEnumerator()).Returns(queryableStudents.GetEnumerator());

            _mockDbSet.Setup(dbSet => dbSet.Add(It.IsAny<Student>())).Callback((Student s) => students.Add(s));

            _mockDbSet.Setup(dbSet => dbSet.Remove(It.IsAny<Student>())).Callback((Student s) => students.Remove(s));

            _mockDbContext.Setup(c => c.Students).Returns(_mockDbSet.Object);

            _repository = new StudentsRepository(_mockDbContext.Object);
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

        [Fact]
        public void ShouldCreateStudent()
        {
            var student = new Student
            {
                LastName = "some-last-name4"
            };

            _repository.CreateStudent(student);

            _mockDbSet.Verify(x => x.Add(student), Times.Exactly(1));

            _mockDbContext.Verify(c => c.SaveChanges(), Times.Exactly(1));

            var addedStudent = _mockDbContext.Object.Students.FirstOrDefault(s => s.LastName == "some-last-name4");

            addedStudent.Should().NotBeNull();
        }

        [Fact]
        public void ShouldThrowExceptionWhenFailingToCreateStudent()
        {
            _mockDbContext.Setup(c => c.SaveChanges()).Throws<Exception>();

            Action action = () => _repository.CreateStudent(new Student());

            action.Should().Throw<Exception>()
                .WithMessage("Student was failed to be saved in the database");
        }

        [Fact]
        public void ShouldReturnFalseWhenNoStudentFoundToUpdate()
        {
            var student = new Student {StudentId = 4};

            var result = _repository.UpdateStudent(student);

            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnTrueWhenStudentFoundToUpdate()
        {
            var student = new Student
                {
                    StudentId = 3,
                    LastName = "new-last-name",
                    FirstMidName = "new-first-mid-name",
                    EnrollmentDate = new DateTime(2000, 1, 2)
                };

            var result = _repository.UpdateStudent(student);

            _mockDbContext.Verify(dbSet => dbSet.SaveChanges(), Times.Exactly(1));

            result.Should().BeTrue();

            var updatedStudent = _mockDbContext.Object.Students.First(s => s.StudentId == 3);

            updatedStudent.LastName.Should().Be("new-last-name");

            updatedStudent.FirstMidName.Should().Be("new-first-mid-name");

            updatedStudent.EnrollmentDate.Should().Be(new DateTime(2000, 1, 2));
        }

        [Fact]
        public void ShouldThrowExceptionWhenFailingToUpdateStudent()
        {
            _mockDbContext.Setup(c => c.SaveChanges()).Throws<Exception>();

            Action action = () => _repository.UpdateStudent(new Student { StudentId = 1 });

            action.Should().Throw<Exception>()
                .WithMessage("Student was failed to be updated in the database");
        }

        [Fact]
        public void ShouldReturnFalseWhenNoStudentFoundToDelete()
        {
            var result = _repository.DeleteStudent(4);

            result.Should().BeFalse();
        }

        [Fact]
        public void ShouldReturnTrueWhenStudentWasDeleted()
        {
            var result = _repository.DeleteStudent(3);

            _mockDbSet.Verify(dbSet => dbSet.Remove(It.IsAny<Student>()), Times.Exactly(1));

            _mockDbContext.Verify(dbSet => dbSet.SaveChanges(), Times.Exactly(1));

            var deletedStudent = _mockDbContext.Object.Students.FirstOrDefault(s => s.StudentId == 3);

            deletedStudent.Should().BeNull();

            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldThrowExceptionWhenFailingToDeleteStudent()
        {
            _mockDbContext.Setup(c => c.SaveChanges()).Throws<Exception>();

            Action action = () => _repository.DeleteStudent(3);

            action.Should().Throw<Exception>()
                .WithMessage("Student was failed to be removed in the database");
        }
    }
}
