using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Department
{
    [Trait("Category", "Unit Test: Data.Repositories.Department")]
    public class DepartmentUpdatedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly DepartmentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public DepartmentUpdatedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldUpdatePrimaryPropertiesWhenCallingUpdate()
        {
            var department = new DepartmentEntity
            {
                DepartmentId = 1,
                Budget = 9999,
                Name = "update-name",
                StartDate = new DateTime(1000, 1, 1),
                CreatedBy = "update-user1",
                CreatedDate = new DateTime(2005, 7, 1),
                UpdatedBy = "update-user2",
                UpdatedDate = new DateTime(2010, 7, 1)
            };

            _repository.Update(department);

            _repository.Save();

            var updatedDepartment = _fixture.Context.Departments.Find(1);

            updatedDepartment.Should().NotBeNull();
            updatedDepartment.Name.Should().Be("update-name");
            updatedDepartment.Budget.Should().Be(9999);
            updatedDepartment.StartDate.Should().Be(new DateTime(1000, 1, 1));
            updatedDepartment.CreatedBy.Should().Be("update-user1");
            updatedDepartment.CreatedDate.Should().Be(new DateTime(2005, 7, 1));
            updatedDepartment.UpdatedBy.Should().Be("update-user2");
            updatedDepartment.UpdatedDate.Should().Be(new DateTime(2010, 7, 1));
        }

        [Fact]
        public void ShouldNotUpdateCoursesWhenCallingUpdate()
        {
            var department = new DepartmentEntity
            {
                DepartmentId = 1,
                Courses = new List<CourseEntity> { new CourseEntity { CourseId = 2 } }
            };

            _repository.Update(department);

            _repository.Save();

            var updatedDepartment = _fixture.Context.Departments.Include(d => d.Courses)
                .FirstOrDefault(d => d.DepartmentId == 1);

            updatedDepartment.Should().NotBeNull();
            updatedDepartment.Courses.All(c => c.CourseId == 1).Should().BeTrue();
        }

        [Fact]
        public void ShouldUpdateAdministratorWhenCallingUpdate()
        {
            var updateToDepartment = new DepartmentEntity
            {
                DepartmentId = 1,
                InstructorId = 2
            };

            _repository.Update(updateToDepartment);

            _repository.Save();

            var updatedDepartment = _fixture.Context.Departments.Include(d => d.Administrator)
                .FirstOrDefault(d => d.DepartmentId == 1);

            updatedDepartment.Should().NotBeNull();
            updatedDepartment.InstructorId.Should().Be(2);
            updatedDepartment.Administrator.InstructorId.Should().Be(2);
            _fixture.Context.Instructors.Count().Should().Be(2);
        }
    }
}
