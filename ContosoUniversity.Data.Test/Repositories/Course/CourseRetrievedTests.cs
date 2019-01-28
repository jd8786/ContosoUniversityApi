using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using System;
using System.Linq;
using ContosoUniversity.Data.Test.Fixtures;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Course
{
    [Trait("Category", "Unit Test: Data.Repositories.Course")]
    public class CourseRetrievedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly ICourseRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public CourseRetrievedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldReturnAllCoursesWhenCallingGetAll()
        {
            var courses = _repository.GetAll().ToList();

            courses.Count.Should().Be(2);
        }

        [Fact]
        public void ShouldIncludeEnrollmentsWhenCallingGetAll()
        {
            var courses = _repository.GetAll().ToList();

            courses[0].Enrollments.Any(e => e.Student.StudentId == 1).Should().BeTrue();
            courses[1].Enrollments.Any(e => e.Student.StudentId == 2).Should().BeTrue();
        }

        [Fact]
        public void ShouldIncludeDepartmentWhenCallingGetAll()
        {
            var courses = _repository.GetAll().ToList();

            courses[0].Department.DepartmentId.Should().Be(1);
            courses[1].Department.DepartmentId.Should().Be(2);
        }

        [Fact]
        public void ShouldIncludeCourseAssignmentsWhenCallingGetAll()
        {
            var courses = _repository.GetAll().ToList();

            courses[0].CourseAssignments.Any(ca => ca.Instructor.InstructorId == 1).Should().BeTrue();
            courses[1].CourseAssignments.Any(ca => ca.Instructor.InstructorId == 2).Should().BeTrue();
        }
    }
}
