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
    public class InstructorUpdatedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly InstructorRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public InstructorUpdatedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldUpdatePrimaryPropertiesWhenCallingUpdate()
        {
            var instructorToUpdate = new InstructorEntity
            {
                InstructorId = 1,
                LastName = "update-last-name",
                FirstMidName = "update-first-mid-name",
                HireDate = new DateTime(2019, 2, 2),
                CreatedBy = "update-user1",
                CreatedDate = new DateTime(2005, 7, 1),
                UpdatedBy = "update-user2",
                UpdatedDate = new DateTime(2010, 7, 1),
            };

            _repository.Update(instructorToUpdate);

            _repository.Save();

            var updatedInstructor = _fixture.Context.Instructors.Find(1);

            updatedInstructor.Should().NotBeNull();
            updatedInstructor.LastName.Should().Be("update-last-name");
            updatedInstructor.FirstMidName.Should().Be("update-first-mid-name");
            updatedInstructor.HireDate.Should().Be(new DateTime(2019, 2, 2));
            updatedInstructor.CreatedBy.Should().Be("update-user1");
            updatedInstructor.CreatedDate.Should().Be(new DateTime(2005, 7, 1));
            updatedInstructor.UpdatedBy.Should().Be("update-user2");
            updatedInstructor.UpdatedDate.Should().Be(new DateTime(2010, 7, 1));
        }

        [Fact]
        public void ShouldAddCourseAssignmentToInstructorWhenCallingUpdate()
        {
            var instructorToUpdate = new InstructorEntity
            {
                InstructorId = 1,
                CourseAssignments = new List<CourseAssignmentEntity>
                {
                    new CourseAssignmentEntity()
                    {
                        CourseId = 1,
                        InstructorId = 1
                    },
                    new CourseAssignmentEntity
                    {
                        CourseId = 2,
                        InstructorId = 1
                    }
                }
            };

            _repository.Update(instructorToUpdate);

            _repository.Save();

            var updatedInstructor = _fixture.Context.Instructors.Include(i => i.CourseAssignments).FirstOrDefault(i => i.InstructorId == 1);

            updatedInstructor.Should().NotBeNull();
            updatedInstructor.CourseAssignments.Count.Should().Be(2);
            updatedInstructor.CourseAssignments.Any(ca => ca.InstructorId == 1 && ca.CourseId == 2).Should().BeTrue();
            _fixture.Context.CourseAssignments.Count(ca => ca.InstructorId == 1 && ca.CourseId == 2).Should().Be(1);
        }

        [Fact]
        public void ShouldRemoveCourseAssignmentFromInstructorWhenCallingUpdate()
        {
            var instructorToUpdate = new InstructorEntity
            {
                InstructorId = 1,
            };

            _repository.Update(instructorToUpdate);

            _repository.Save();

            var updatedInstructor = _fixture.Context.Instructors.Include(i => i.CourseAssignments).FirstOrDefault(i => i.InstructorId == 1);

            updatedInstructor.Should().NotBeNull();
            updatedInstructor.CourseAssignments.Should().BeNullOrEmpty();
            _fixture.Context.CourseAssignments.Any(ca => ca.CourseId == 1 && ca.InstructorId == 1).Should().BeFalse();
        }

        [Fact]
        public void ShouldAddAndRemoveCourseAssignmentToInstructorWhenCallingUpdate()
        {
            var instructorToUpdate = new InstructorEntity
            {
                InstructorId = 1,
                CourseAssignments = new List<CourseAssignmentEntity>
                {
                    new CourseAssignmentEntity
                    {
                        CourseId = 2,
                        InstructorId = 1
                    }
                }
            };

            _repository.Update(instructorToUpdate);

            _repository.Save();

            var updatedInstructor = _fixture.Context.Instructors.Include(i => i.CourseAssignments).FirstOrDefault(i => i.InstructorId == 1);

            updatedInstructor.Should().NotBeNull();
            updatedInstructor.CourseAssignments.All(ca => ca.CourseId == 2 && ca.InstructorId == 1).Should().BeTrue();
            _fixture.Context.CourseAssignments.Any(ca => ca.InstructorId == 1 && ca.CourseId != 2).Should().BeFalse();
        }

        [Fact]
        public void ShouldUpdateOfficeAssignmentLocationPropertyWhenCallingUpdate()
        {
            var instructorToUpdate = new InstructorEntity
            {
                InstructorId = 1,
                OfficeAssignment = new OfficeAssignmentEntity { InstructorId = 1, Location = "update-location" }
            };

            _repository.Update(instructorToUpdate);

            _repository.Save();

            var updatedInstructor = _fixture.Context.Instructors.Include(i => i.OfficeAssignment).FirstOrDefault(i => i.InstructorId == 1);

            updatedInstructor.Should().NotBeNull();
            updatedInstructor.OfficeAssignment.InstructorId.Should().Be(1);
            updatedInstructor.OfficeAssignment.Location.Should().Be("update-location");
            _fixture.Context.OfficeAssignments.Find(1).Location.Should().Be("update-location");
        }

        [Fact]
        public void ShouldRemoveAndAddOfficeAssignmentWhenCallingUpdate()
        {
            var instructorToUpdate = new InstructorEntity
            {
                InstructorId = 1,
            };

            _repository.Update(instructorToUpdate);

            _repository.Save();

            var updatedInstructor1 = _repository.Context.Instructors.Include(i => i.OfficeAssignment).FirstOrDefault(i => i.InstructorId == 1);

            updatedInstructor1.Should().NotBeNull();
            updatedInstructor1.OfficeAssignment.Should().BeNull();
            _fixture.Context.OfficeAssignments.Any(o => o.Location == "location1").Should().BeFalse();

            updatedInstructor1.OfficeAssignment = new OfficeAssignmentEntity { Location = "new-location" };

            _repository.Save();

            var updatedInstructor2 = _repository.Context.Instructors.Include(i => i.OfficeAssignment).FirstOrDefault(i => i.InstructorId == 1);

            updatedInstructor2.Should().NotBeNull();
            updatedInstructor2.OfficeAssignment.InstructorId.Should().Be(1);
            _fixture.Context.OfficeAssignments.Any(o => o.Location == "new-location").Should().BeTrue();
        }
    }
}
