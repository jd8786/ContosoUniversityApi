using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Instructor
{
    [Trait("Category", "Unit Test: Data.Repositories.Instructor")]
    public class InstructorCreatedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly InstructorRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public InstructorCreatedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldCreateInstructorWhenCallingAdd()
        {
            var instructor = new InstructorEntity
            {
                InstructorId = 3,
                LastName = "new-last-name",
                FirstMidName = "new-first-mide-name"
            };

            _repository.Add(instructor);

            _repository.Save();

            _fixture.Context.Instructors.Count(i => i.InstructorId == 3).Should().Be(1);
        }

        [Fact]
        public void ShouldCreateAListOfInstructorsWhenCallingAddRange()
        {
            var instructor1 = new InstructorEntity
            {
                InstructorId = 3,
                LastName = "new-last-name1",
                FirstMidName = "new-first-mide-name1"
            };

            var instructor2 = new InstructorEntity
            {
                InstructorId = 4,
                LastName = "new-last-name2",
                FirstMidName = "new-first-mide-name2"
            };

            _repository.AddRange(new List<InstructorEntity> { instructor1, instructor2 });

            _repository.Save();

            _fixture.Context.Instructors.Count(i => i.InstructorId == 3).Should().Be(1);
            _fixture.Context.Instructors.Count(i => i.InstructorId == 4).Should().Be(1);
        }

        [Fact]
        public void ShouldCreateCourseAssignmentWhenCallingAdd()
        {
            var instructor = new InstructorEntity
            {
                InstructorId = 3,
                LastName = "new-last-name",
                FirstMidName = "new-first-mide-name",
                CourseAssignments = new List<CourseAssignmentEntity>
                {
                    new CourseAssignmentEntity
                    {
                        InstructorId = 3,
                        CourseId = 1
                    }
                }
            };

            _repository.Add(instructor);

            _repository.Save();

            var addedInstructor = _fixture.Context.Instructors.Include(i => i.CourseAssignments).FirstOrDefault(i => i.InstructorId == 3);

            addedInstructor.Should().NotBeNull();
            addedInstructor.CourseAssignments.Count(ca => ca.CourseId == 1 && ca.InstructorId == 3).Should().Be(1);
            _fixture.Context.CourseAssignments.Count(ca => ca.CourseId == 1 && ca.InstructorId == 3).Should().Be(1);
        }

        [Fact]
        public void ShouldCreateOfficeAssignmentWhenCallingAdd()
        {
            var instructor = new InstructorEntity
            {
                InstructorId = 3,
                LastName = "new-last-name",
                FirstMidName = "new-first-mide-name",
                OfficeAssignment = new OfficeAssignmentEntity
                {
                    Location = "new-location"
                }
            };

            _repository.Add(instructor);

            _repository.Save();

            var addedInstructor = _fixture.Context.Instructors.Include(i => i.OfficeAssignment).FirstOrDefault(i => i.InstructorId == 3);

            addedInstructor.Should().NotBeNull();
            addedInstructor.OfficeAssignment.InstructorId.Should().Be(3);
            _fixture.Context.OfficeAssignments.Any(o => o.InstructorId == 3).Should().BeTrue();
        }
    }
}
