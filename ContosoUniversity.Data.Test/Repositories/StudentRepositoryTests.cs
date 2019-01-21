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
    public class StudentRepositoryTests : IClassFixture<UnitTestFixture>, IDisposable
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
            var student = new StudentEntity { StudentId = 1 };

            _repository.Remove(student);

            _repository.Save();

            _fixture.Context.Students.Any(s => s.StudentId == 1).Should().BeFalse();
        }

        [Fact]
        public void ShouldRemoveAListOfStudentsWhenCallingRemoveArrange()
        {
            var students = new List<StudentEntity>
            {
                new StudentEntity { StudentId = 1 },
                new StudentEntity { StudentId = 2 }
            };

            _repository.RemoveRange(students);

            _repository.Save();

            _fixture.Context.Students.Any().Should().BeFalse();
        }

        [Fact]
        public void ShouldCreateStudentWhenCallingAdd()
        {
            var student = new StudentEntity
            {
                StudentId = 3,
                LastName = "some-last-name"
            };

             _repository.Add(student);

            _repository.Save();

            _fixture.Context.Students.Count(s => s.StudentId == 3).Should().Be(1);
        }

        [Fact]
        public void ShouldCreateAListOfStudentsWhenCallingAddRange()
        {
            var student1 = new StudentEntity
            {
                StudentId = 3,
                LastName = "some-last-name1"
            };

            var student2 = new StudentEntity
            {
                StudentId = 4,
                LastName = "some-last-name2"
            };

            _repository.AddRange(new List<StudentEntity>{ student1, student2});

            _repository.Save();

            _fixture.Context.Students.Count(s => s.StudentId == 3).Should().Be(1);
            _fixture.Context.Students.Count(s => s.StudentId == 4).Should().Be(1);
        }

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
