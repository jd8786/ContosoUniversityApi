using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories
{
    [Trait("Category", "Unit Test: Data.Repositories.Students")]
    public class StudentRepositoryTests: IClassFixture<UnitTestFixture>, IDisposable
    {
        private readonly IStudentsRepository _repository;
        private readonly UnitTestFixture _fixture;

        public StudentRepositoryTests(UnitTestFixture fixture)
        {
            _fixture = fixture;

            _fixture.InitData();

            _repository = new StudentsRepository(_fixture.Context);
        }

        [Fact]
        public void ShouldReturnAllTheStudentsWhenCallingGetAll()
        {
            var students = _repository.GetAll().ToList();

            students.Count.Should().Be(2);
        }

        [Fact]
        public void ShouldIncludeAllTheChildrenWhenCallingGetAll()
        {
            var students = _repository.GetAll().ToList();

            students[0].Enrollments.Any(e => e.CourseId == 1 && e.StudentId == 1).Should().BeTrue();
            students[1].Enrollments.Any(e => e.CourseId == 2 && e.StudentId == 2).Should().BeTrue();
        }

        [Fact]
        public void ShouldRemoveStudentWhenCallingRemove()
        {
            var student = new StudentEntity {StudentId = 1};

            _repository.Remove(student);

            _repository.Save();

            _fixture.Context.Students.Any(s => s.StudentId == 1).Should().BeFalse();
        }

        //[Fact]
        //public void ShouldReturnStudentWhenIdExists()
        //{
        //    var student = _repository.Get(2);

        //    student.Should().NotBeNull();

        //    student.LastName.Should().Be("some-last-name2");
        //}

        //[Fact]
        //public void ShouldReturnNullWhenIdDoesNotExist()
        //{
        //    var student = await _repository.GetStudentByIdAsync(4);

        //    student.Should().BeNull();
        //}

        //[Fact]
        //public async Task ShouldCreateStudent()
        //{
        //    var student = new StudentEntity
        //    {
        //        LastName = "some-last-name4"
        //    };

        //    await _repository.CreateAsync(student);

        //    _mockDbSet.Verify(x => x.Add(student), Times.Exactly(1));

        //    _mockDbContext.Verify(c => c.SaveChanges(), Times.Exactly(1));

        //    var addedStudent = _mockDbContext.Object.Students.FirstOrDefault(s => s.LastName == "some-last-name4");

        //    addedStudent.Should().NotBeNull();
        //}

        //[Fact]
        //public void ShouldThrowExceptionWhenFailingToCreateStudent()
        //{
        //    _mockDbContext.Setup(c => c.SaveChanges()).Throws<Exception>();

        //    Action action = async () => await _repository.CreateAsync(new StudentEntity());

        //    action.Should().Throw<Exception>()
        //        .WithMessage("Student was failed to be saved in the database");
        //}

        //[Fact]
        //public async Task ShouldReturnFalseWhenNoStudentFoundToUpdate()
        //{
        //    var student = new StudentEntity { StudentId = 4 };

        //    var result = await _repository.UpdateAsync(student);

        //    result.Should().BeFalse();
        //}

        //[Fact]
        //public async Task ShouldReturnTrueWhenStudentFoundToUpdate()
        //{
        //    var student = new StudentEntity
        //    {
        //        StudentId = 3,
        //        LastName = "new-last-name",
        //        FirstMidName = "new-first-mid-name",
        //        EnrollmentDate = new DateTime(2000, 1, 2)
        //    };

        //    var result = await _repository.UpdateAsync(student);

        //    _mockDbContext.Verify(dbSet => dbSet.SaveChanges(), Times.Exactly(1));

        //    result.Should().BeTrue();

        //    var updatedStudent = _mockDbContext.Object.Students.First(s => s.StudentId == 3);

        //    updatedStudent.LastName.Should().Be("new-last-name");

        //    updatedStudent.FirstMidName.Should().Be("new-first-mid-name");

        //    updatedStudent.EnrollmentDate.Should().Be(new DateTime(2000, 1, 2));
        //}

        //[Fact]
        //public void ShouldThrowExceptionWhenFailingToUpdateStudent()
        //{
        //    _mockDbContext.Setup(c => c.SaveChanges()).Throws<Exception>();

        //    Action action = async () => await _repository.UpdateAsync(new StudentEntity { StudentId = 1 });

        //    action.Should().Throw<Exception>()
        //        .WithMessage("Student was failed to be updated in the database");
        //}

        //[Fact]
        //public async Task ShouldReturnFalseWhenNoStudentFoundToDelete()
        //{
        //    var result = await _repository.DeleteAsync(4);

        //    result.Should().BeFalse();
        //}

        //[Fact]
        //public async Task ShouldReturnTrueWhenStudentWasDeleted()
        //{
        //    var result = await _repository.DeleteAsync(3);

        //    _mockDbSet.Verify(dbSet => dbSet.Remove(It.IsAny<StudentEntity>()), Times.Exactly(1));

        //    _mockDbContext.Verify(dbSet => dbSet.SaveChanges(), Times.Exactly(1));

        //    var deletedStudent = _mockDbContext.Object.Students.FirstOrDefault(s => s.StudentId == 3);

        //    deletedStudent.Should().BeNull();

        //    result.Should().BeTrue();
        //}

        //[Fact]
        //public void ShouldThrowExceptionWhenFailingToDeleteStudent()
        //{
        //    _mockDbContext.Setup(c => c.SaveChanges()).Throws<Exception>();

        //    Action action = async () => await _repository.DeleteAsync(3);

        //    action.Should().Throw<Exception>()
        //        .WithMessage("Student was failed to be removed in the database");
        //}
        public void Dispose()
        {
            _fixture.Dispose();
        }
    }
}
