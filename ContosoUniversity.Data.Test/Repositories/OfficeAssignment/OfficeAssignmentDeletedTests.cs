using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.OfficeAssignment
{
    [Trait("Category", "Unit Test: Data.Repositories.OfficeAssignment")]
    public class OfficeAssignmentDeletedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly OfficeAssignmentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public OfficeAssignmentDeletedTests(InMemoryDbTestFixture fixture)
        {
            _fixture = fixture;

            _fixture.InitData();

            _repository = new OfficeAssignmentRepository(_fixture.Context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public void ShouldRemoveOfficeAssignmentWhenCallingRemove()
        {
            var officeAssignment = _repository.Context.OfficeAssignments.Find(1);

            _repository.Remove(officeAssignment);

            _repository.Save();

            _fixture.Context.OfficeAssignments.Any(o => o.InstructorId == 1).Should().BeFalse();
        }

        [Fact]
        public void ShouldRemoveAListOfOfficeAssignmentsWhenCallingRemoveArrange()
        {
            var officeAssignments = _repository.Context.OfficeAssignments;

            _repository.RemoveRange(officeAssignments);

            _repository.Save();

            _fixture.Context.OfficeAssignments.Any(o => o.InstructorId == 1 || o.InstructorId == 2).Should().BeFalse();
        }

        [Fact]
        public void ShouldNotRemoveInstructorWhenOfficeAssignmentIsRemoved()
        {
            var officeAssignment = _repository.Context.OfficeAssignments.Find(1);

            _repository.Remove(officeAssignment);

            _repository.Save();

            _fixture.Context.OfficeAssignments.Any(o => o.InstructorId == 1).Should().BeFalse();
            _fixture.Context.Instructors.Count().Should().Be(2);
            _fixture.Context.Instructors.Find(1).Should().NotBeNull();
        }
    }
}
