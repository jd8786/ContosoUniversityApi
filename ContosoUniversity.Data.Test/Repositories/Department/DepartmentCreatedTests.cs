﻿using ContosoUniversity.Data.EntityModels;
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
    public class DepartmentCreatedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly DepartmentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public DepartmentCreatedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldCreateDepartmentWhenCallingAdd()
        {
            var department = new DepartmentEntity
            {
                DepartmentId = 3,
                Name = "some-name"
            };

            _repository.Add(department);

            _repository.Save();

            _fixture.Context.Departments.Count(d => d.DepartmentId == 3).Should().Be(1);
        }

        [Fact]
        public void ShouldCreateAListOfDepartmentsWhenCallingAddRange()
        {
            var department1 = new DepartmentEntity
            {
                DepartmentId = 3,
                Name = "some-name1"
            };

            var department2 = new DepartmentEntity
            {
                DepartmentId = 4,
                Name = "some-name2"
            };

            _repository.AddRange(new List<DepartmentEntity> { department1, department2 });

            _repository.Save();

            _fixture.Context.Departments.Count(d => d.DepartmentId == 3).Should().Be(1);
            _fixture.Context.Departments.Count(d => d.DepartmentId == 4).Should().Be(1);
        }

        [Fact]
        public void ShouldNotCreateAdminstratorWhenCallingAdd()
        {
            var department = new DepartmentEntity
            {
                DepartmentId = 3,
                Name = "some-name",
                InstructorId = 1
            };

            _repository.Add(department);

            _repository.Save();

            var addedDepartment = _fixture.Context.Departments.Include(d => d.Administrator).FirstOrDefault(d => d.DepartmentId == 3);

            addedDepartment.Should().NotBeNull();
            addedDepartment.Administrator.InstructorId.Should().Be(1);
            _fixture.Context.Instructors.Count().Should().Be(2);
        }

        [Fact]
        public void ShouldNotCreateCoursesWhenCallingAdd()
        {
            var course1 = new CourseEntity { Title = "title1" };
            var course2 = new CourseEntity { Title = "title2" };

            var department = new DepartmentEntity
            {
                DepartmentId = 3,
                Name = "some-name",
                Courses = new List<CourseEntity> { course1, course2 }
            };

            _repository.Add(department);

            _repository.Save();

            var addedDepartment = _fixture.Context.Departments.FirstOrDefault(d => d.DepartmentId == 3);

            addedDepartment.Should().NotBeNull();
            addedDepartment.Courses.Should().BeNull();
            _fixture.Context.Courses.Count().Should().Be(2);
        }
    }
}
