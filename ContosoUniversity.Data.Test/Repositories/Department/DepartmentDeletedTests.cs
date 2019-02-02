using System;
using System.Linq;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Department
{
    [Trait("Category", "Unit Test: Data.Repositories.Department")]
    public class DepartmentDeletedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly DepartmentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public DepartmentDeletedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldRemoveDepartmentWhenCallingRemove()
        {
            var department = _repository.Context.Departments.Find(1);

            _repository.Remove(department);

            _repository.Save();

            _fixture.Context.Departments.Any(d => d.DepartmentId == 1).Should().BeFalse();
        }

        [Fact]
        public void ShouldRemoveAListOfDepartmentsWhenCallingRemoveArrange()
        {
            var departments = _repository.Context.Departments;

            _repository.RemoveRange(departments);

            _repository.Save();

            _fixture.Context.Departments.Any(d => d.DepartmentId == 1 || d.DepartmentId == 2).Should().BeFalse();
        }

        [Fact]
        public void ShouldNotRemoveAdministratorWhenDepartmentIsRemoved()
        {
            var department = _repository.Context.Departments.Find(1);

            _repository.Remove(department);

            _repository.Save();

            _fixture.Context.Departments.Any(d => d.DepartmentId == 1).Should().BeFalse();
            _fixture.Context.Instructors.Any(i => i.InstructorId == 1).Should().BeTrue();
        }

        [Fact(Skip = "InmemoryDb doesn't work for related entity update")]
        public void ShouldRemoveCoursesWhenDepartmentIsRemoved()
        {
            var department = _repository.Context.Departments.Find(1);

            _repository.Remove(department);

            _repository.Save();

            _fixture.Context.Departments.Any(d => d.DepartmentId == 1).Should().BeFalse();

            _fixture.Context.Courses.Any(c => c.CourseId == 1).Should().BeFalse();
        }
    }
}
