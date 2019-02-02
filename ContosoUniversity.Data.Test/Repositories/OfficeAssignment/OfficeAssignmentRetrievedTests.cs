using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.OfficeAssignment
{
    [Trait("Category", "Unit Test: Data.Repositories.OfficeAssignment")]
    public class OfficeAssignmentRetrievedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly OfficeAssignmentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public OfficeAssignmentRetrievedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldReturnAllOfficeAssignmentsWhenCallingGetAll()
        {
            var officeAssignments = _repository.GetAll().ToList();

            officeAssignments.Count.Should().Be(2);
        }

        [Fact]
        public void ShouldIncludeInstructorWhenCallingGetAll()
        {
            var officeAssignments = _repository.GetAll().ToList();

            officeAssignments[0].Instructor.InstructorId.Should().Be(1);
            officeAssignments[1].Instructor.InstructorId.Should().Be(2);
        }
    }
}
