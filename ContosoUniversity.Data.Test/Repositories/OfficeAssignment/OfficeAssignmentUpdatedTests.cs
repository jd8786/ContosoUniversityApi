using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using System;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.OfficeAssignment
{
    [Trait("Category", "Unit Test: Data.Repositories.OfficeAssignment")]
    public class OfficeAssignmentUpdatedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly OfficeAssignmentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public OfficeAssignmentUpdatedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldUpdatePrimaryPropertiesWhenCallingUpdate()
        {
            var officeAssignmentToUpdate = new OfficeAssignmentEntity
            {
                InstructorId = 1,
                Location = "new-location"
            };

            _repository.Update(officeAssignmentToUpdate);

            _repository.Save();

            var updatedOfficeAssignment = _fixture.Context.OfficeAssignments.Find(1);

            updatedOfficeAssignment.Location.Should().Be("new-location");
        }

        [Fact]
        public void ShouldThrowExceptionWhenTryingToUpdateInstructor()
        {
            var existingOfficeAssignment = _repository.Context.OfficeAssignments.Find(1);

            existingOfficeAssignment.InstructorId = 2;

            _repository.Update(existingOfficeAssignment);

            Assert.Throws<InvalidOperationException>(() =>
            {
                _repository.Save();
            });
        }
    }
}
