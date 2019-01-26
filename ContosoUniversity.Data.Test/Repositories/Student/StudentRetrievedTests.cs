using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Student
{
    [Trait("Category", "Unit Test: Data.Repositories.Student")]
    public class StudentRetrievedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly IStudentsRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public StudentRetrievedTests(InMemoryDbTestFixture fixture)
        {
            _fixture = fixture;

            _fixture.InitData();

            _repository = new StudentsRepository(_fixture.Context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public void ShouldReturnAllStudentsWhenCallingGetAll()
        {
            var students = _repository.GetAll().ToList();

            students.Count.Should().Be(2);
        }

        [Fact]
        public void ShouldIncludeAllChildrenWhenCallingGetAll()
        {
            var students = _repository.GetAll().ToList();

            students[0].Enrollments.Any(e => e.CourseId == 1 && e.StudentId == 1).Should().BeTrue();
            students[1].Enrollments.Any(e => e.CourseId == 2 && e.StudentId == 2).Should().BeTrue();
        }
    }
}
