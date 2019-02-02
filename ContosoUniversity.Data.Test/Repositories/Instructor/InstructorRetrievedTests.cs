using System;
using System.Linq;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Instructor
{
    [Trait("Category", "Unit Test: Data.Repositories.Instructor")]
    public class InstructorRetrievedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly InstructorRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public InstructorRetrievedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldReturnAllInstructorsWhenCallingGetAll()
        {
            var instructors = _repository.GetAll().ToList();

            instructors.Count.Should().Be(2);
        }

        [Fact]
        public void ShouldIncludeCourseAssignmentsWhenCallingGetAll()
        {
            var instructors = _repository.GetAll().ToList();

            instructors[0].CourseAssignments.Any(ca => ca.Course.CourseId == 1).Should().BeTrue();
            instructors[1].CourseAssignments.Any(ca => ca.Course.CourseId == 2).Should().BeTrue();
        }

        [Fact]
        public void ShouldIncludeOfficeAssignmentsWhenCallingGetAll()
        {
            var instructors = _repository.GetAll().ToList();

            instructors[0].OfficeAssignment.InstructorId.Should().Be(1);
            instructors[1].OfficeAssignment.InstructorId.Should().Be(2);
        }
    }
}
