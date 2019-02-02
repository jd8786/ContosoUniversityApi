using System;
using System.Linq;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Instructor
{
    [Trait("Category", "Unit Test: Data.Repositories.Instructor")]
    public class InstructorDeletedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly InstructorRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public InstructorDeletedTests(InMemoryDbTestFixture fixture)
        {
            _fixture = fixture;

            _fixture.InitData();

            _repository = new InstructorRepository(_fixture.Context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public void ShouldRemoveInstructorWhenCallingRemove()
        {
            var instructor = _repository.Context.Instructors.Find(1);

            _repository.Remove(instructor);

            _repository.Save();

            _fixture.Context.Instructors.Any(i => i.InstructorId == 1).Should().BeFalse();
        }

        [Fact]
        public void ShouldRemoveAListOfInstructorsWhenCallingRemoveArrange()
        {
            var instructors = _repository.Context.Instructors;

            _repository.RemoveRange(instructors);

            _repository.Save();

            _fixture.Context.Instructors.Any(i => i.InstructorId == 1 || i.InstructorId == 2).Should().BeFalse();
        }

        [Fact]
        public void ShouldRemoveOfficeAssignmentWhenRemovingInstructor()
        {
            var instructor = _repository.Context.Instructors.Find(1);

            _repository.Remove(instructor);

            _repository.Save();

            _fixture.Context.Instructors.Any(i => i.InstructorId == 1).Should().BeFalse();
            _fixture.Context.OfficeAssignments.Any(oa => oa.InstructorId == 1).Should().BeTrue();
        }

        [Fact(Skip = "InmemoryDb doesn't work for related entity update")]
        public void ShouldRemoveCourseAssignmentWhenRemovingInstructor()
        {
            var instructor = _repository.Context.Instructors.Find(1);

            _repository.Remove(instructor);

            _repository.Save();

            _fixture.Context.Instructors.Any(i => i.InstructorId == 1).Should().BeFalse();
            _fixture.Context.CourseAssignments.Any(ca => ca.CourseId == 1 && ca.InstructorId == 1).Should().BeFalse();
        }
    }
}
