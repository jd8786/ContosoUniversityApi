using System;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.OfficeAssignment
{
    [Trait("Category", "Unit Test: Data.Repositories.OfficeAssignment")]
    public class OfficeAssignmentCreatedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly OfficeAssignmentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public OfficeAssignmentCreatedTests(InMemoryDbTestFixture fixture)
        {
            _fixture = fixture;

            _fixture.InitData();

            _repository = new OfficeAssignmentRepository(_fixture.Context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact(Skip = "InmemoryDb doesn't work for related entity update")]
        public void ShouldThrowExceptionWhenTryingToAddOfficeAssignment()
        {
            var officeAssignment = new OfficeAssignmentEntity
            {
                InstructorId = 3,
                Location = "new-location"
            };

            _repository.Add(officeAssignment);

            Assert.Throws<Exception>(() => _repository.Save());
        }
    }
}
