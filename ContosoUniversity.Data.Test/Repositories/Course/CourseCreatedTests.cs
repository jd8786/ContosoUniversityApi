using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.Test.Fixtures;
using Xunit;
using CourseAssignmentEntity = ContosoUniversity.Data.EntityModels.CourseAssignmentEntity;

namespace ContosoUniversity.Data.Test.Repositories.Course
{
    [Trait("Category", "Unit Test: Data.Repositories.Course")]
    public class CourseCreatedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly CourseRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public CourseCreatedTests(InMemoryDbTestFixture fixture)
        {
            _fixture = fixture;

            _fixture.InitData();

            _repository = new CourseRepository(_fixture.Context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public void ShouldCreateCourseWhenCallingAdd()
        {
            var course = new CourseEntity
            {
                CourseId = 3,
                Title = "new-title"
            };

            _repository.Add(course);

            _repository.Save();

            _fixture.Context.Courses.Count(c => c.CourseId == 3).Should().Be(1);
        }

        [Fact]
        public void ShouldCreateAListOfCoursesWhenCallingAddRange()
        {
            var course1 = new CourseEntity
            {
                CourseId = 3,
                Title = "new-title1"
            };

            var course2 = new CourseEntity
            {
                CourseId = 4,
                Title = "new-title2"
            };

            _repository.AddRange(new List<CourseEntity> { course1, course2 });

            _repository.Save();

            _fixture.Context.Courses.Count(c => c.CourseId == 3).Should().Be(1);
            _fixture.Context.Courses.Count(c => c.CourseId == 4).Should().Be(1);
        }

        [Fact]
        public void ShouldCreateEnrollmentWhenCallingAdd()
        {
            var course = new CourseEntity
            {
                CourseId = 3,
                Title = "new-title",
                Enrollments = new List<EnrollmentEntity>
                {
                    new EnrollmentEntity
                    {
                        CourseId = 3,
                        StudentId = 1,
                    }
                }
            };

            _repository.Add(course);

            _repository.Save();

            var addedCourse = _fixture.Context.Courses.Include(c => c.Enrollments).FirstOrDefault(s => s.CourseId == 3);

            addedCourse.Should().NotBeNull();

            addedCourse.Enrollments.Count(e => e.CourseId == 3 && e.StudentId == 1).Should().Be(1);
            _fixture.Context.Enrollments.Count(e => e.CourseId == 3 && e.StudentId == 1).Should().Be(1);
        }

        [Fact]
        public void ShouldCreateCourseAssignmentWhenCallingAdd()
        {
            var course = new CourseEntity
            {
                CourseId = 3,
                Title = "new-title",
                CourseAssignments = new List<CourseAssignmentEntity>
                {
                    new CourseAssignmentEntity
                    {
                        CourseId = 3,
                        InstructorId = 1,
                    }
                }
            };

            _repository.Add(course);

            _repository.Save();

            var addedCourse = _fixture.Context.Courses.Include(c => c.CourseAssignments).FirstOrDefault(s => s.CourseId == 3);

            addedCourse.Should().NotBeNull();
            addedCourse.CourseAssignments.Count(e => e.CourseId == 3 && e.InstructorId == 1).Should().Be(1);
            _fixture.Context.CourseAssignments.Count(e => e.CourseId == 3 && e.InstructorId == 1).Should().Be(1);
        }

        [Fact]
        public void ShouldNotCreateDepartmentWhenCallingAdd()
        {
            var course = new CourseEntity
            {
                CourseId = 3,
                Title = "new-title",
                DepartmentId = 1
            };

            _repository.Add(course);

            _repository.Save();

            var addedCourse = _fixture.Context.Courses.Include(c => c.Department).FirstOrDefault(s => s.CourseId == 3);

            addedCourse.Should().NotBeNull();
            addedCourse.Department.DepartmentId.Should().Be(1);
            _fixture.Context.Departments.Count().Should().Be(2);
        }
    }
}
