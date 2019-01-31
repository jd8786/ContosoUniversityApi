using System;
using System.Linq;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Department
{
    [Trait("Category", "Unit Test: Data.Repositories.Department")]
    public class DepartmentRetrievedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly DepartmentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public DepartmentRetrievedTests(InMemoryDbTestFixture fixture)
        {
            _fixture = fixture;

            _fixture.InitData();

            _repository = new DepartmentRepository(_fixture.Context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public void ShouldReturnAllDepartmentsWhenCallingGetAll()
        {
            var departments = _repository.GetAll().ToList();

            departments.Count.Should().Be(2);
        }

        [Fact]
        public void ShouldIncludeCoursesWhenCallingGetAll()
        {
            var departments = _repository.GetAll().ToList();

            departments[0].Courses.Any(c => c.CourseId == 1).Should().BeTrue();
            departments[1].Courses.Any(c => c.CourseId == 2).Should().BeTrue();
        }

        [Fact]
        public void ShouldIncludeAdministratorWhenCallingGetAll()
        {
            var departments = _repository.GetAll().ToList();

            departments[0].Administrator.InstructorId.Should().Be(1);
            departments[1].Administrator.InstructorId.Should().Be(2);
        }
    }
}
