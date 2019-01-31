using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Student
{
    [Trait("Category", "Unit Test: Data.Repositories.Student")]
    public class StudentDeletedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly StudentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public StudentDeletedTests(InMemoryDbTestFixture fixture)
        {
            _fixture = fixture;

            _fixture.InitData();

            _repository = new StudentRepository(_fixture.Context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public void ShouldRemoveStudentWhenCallingRemove()
        {
            var student = _repository.Context.Students.Find(1);

            _repository.Remove(student);

            _repository.Save();

            _fixture.Context.Students.Any(s => s.StudentId == 1).Should().BeFalse();
        }

        [Fact]
        public void ShouldRemoveAListOfStudentsWhenCallingRemoveArrange()
        {
            var students = _repository.Context.Students;

            _repository.RemoveRange(students);

            _repository.Save();

            _fixture.Context.Students.Any(s => s.StudentId == 1 || s.StudentId == 2).Should().BeFalse();
        }

        [Fact(Skip = "InmemoryDb doesn't work for related entity update")]
        public void ShouldRemoveEnrollmentsWhenStudentIsRemoved()
        {
            var student = _repository.Context.Students.Find(1);

            _repository.Remove(student);

            _repository.Save();

            _fixture.Context.Students.Any(s => s.StudentId == 1).Should().BeFalse();
            _fixture.Context.Enrollments.Any(e => e.StudentId == 1).Should().BeFalse();
        }
    }
}
