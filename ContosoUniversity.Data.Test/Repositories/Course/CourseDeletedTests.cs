using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Course
{
    [Trait("Category", "Unit Test: Data.Repositories.Course")]
    public class CourseDeletedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly CourseRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public CourseDeletedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldRemoveCourseWhenCallingRemove()
        {
            var course = _repository.Context.Courses.Find(1);

            _repository.Remove(course);

            _repository.Save();

            _fixture.Context.Courses.Any(c => c.CourseId == 1).Should().BeFalse();
        }

        [Fact]
        public void ShouldRemoveAListOfCoursesWhenCallingRemoveArrange()
        {
            var courses = _repository.Context.Courses;

            _repository.RemoveRange(courses);

            _repository.Save();

            _fixture.Context.Courses.Any(c => c.CourseId == 1 || c.CourseId == 2).Should().BeFalse();
        }

        [Fact]
        public void ShouldNotRemoveDepartmentWhenRemovingCourse()
        {
            var course = _repository.Context.Courses.Find(1);

            _repository.Remove(course);

            _repository.Save();

            _fixture.Context.Courses.Any(c => c.CourseId == 1).Should().BeFalse();
            _fixture.Context.Departments.Any(d => d.DepartmentId == 1).Should().BeTrue();
        }

        [Fact(Skip = "InmemoryDb doesn't work for related entity update")]
        public void ShouldRemoveEnrollmentWhenRemovingCourse()
        {
            var course = _repository.Context.Courses.Find(1);

            _repository.Remove(course);

            _repository.Save();

            _fixture.Context.Courses.Any(c => c.CourseId == 1).Should().BeFalse();
            _fixture.Context.Enrollments.Any(c => c.CourseId == 1 && c.StudentId == 1).Should().BeFalse();
        }

        [Fact(Skip = "InmemoryDb doesn't work for related entity update")]
        public void ShouldRemoveCourseAssignmentWhenRemovingCourse()
        {
            var course = _repository.Context.Courses.Find(1);

            _repository.Remove(course);

            _repository.Save();

            _fixture.Context.Courses.Any(c => c.CourseId == 1).Should().BeFalse();
            _fixture.Context.CourseAssignments.Any(c => c.CourseId == 1 && c.InstructorId == 1).Should().BeFalse();
        }
    }
}
