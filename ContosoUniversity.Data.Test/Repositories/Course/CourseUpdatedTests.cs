using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Course
{
    [Trait("Category", "Unit Test: Data.Repositories.Course")]
    public class CourseUpdatedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly CourseRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public CourseUpdatedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldUpdatePrimaryPropertiesWhenCallingUpdate()
        {
            var courseToUpdate = new CourseEntity
            {
                CourseId = 1,
                Title = "update-title",
                Credits = 8,
                CreatedBy = "update-user1",
                CreatedDate = new DateTime(2005, 7, 1),
                UpdatedBy = "update-user2",
                UpdatedDate = new DateTime(2010, 7, 1),
            };

            _repository.Update(courseToUpdate);

            _repository.Save();

            var updatedStudent = _fixture.Context.Courses.Find(1);

            updatedStudent.Should().NotBeNull();
            updatedStudent.Title.Should().Be("update-title");
            updatedStudent.Credits.Should().Be(8);
            updatedStudent.CreatedBy.Should().Be("update-user1");
            updatedStudent.CreatedDate.Should().Be(new DateTime(2005, 7, 1));
            updatedStudent.UpdatedBy.Should().Be("update-user2");
            updatedStudent.UpdatedDate.Should().Be(new DateTime(2010, 7, 1));
        }

        [Fact]
        public void ShouldAddEnrollmentToCourseWhenCallingUpdate()
        {
            var courseToUpdate = new CourseEntity
            {
                CourseId = 1,
                Enrollments = new List<EnrollmentEntity>
                {
                    new EnrollmentEntity
                    {
                        CourseId = 1,
                        StudentId = 1
                    },
                    new EnrollmentEntity
                    {
                        CourseId = 1,
                        StudentId = 2
                    }
                }
            };

            _repository.Update(courseToUpdate);

            _repository.Save();

            var updatedCourse = _fixture.Context.Courses.Include(c => c.Enrollments).FirstOrDefault(c => c.CourseId == 1);

            updatedCourse.Should().NotBeNull();
            updatedCourse.Enrollments.Count.Should().Be(2);
            updatedCourse.Enrollments.Any(e => e.StudentId == 2 && e.CourseId == 1).Should().BeTrue();
            _fixture.Context.Enrollments.Count(e => e.StudentId == 2 && e.CourseId == 1).Should().Be(1);
        }

        [Fact]
        public void ShouldRemoveEnrollmentFromCourseWhenCallingUpdate()
        {
            var courseToUpdate = new CourseEntity
            {
                CourseId = 1,
            };

            _repository.Update(courseToUpdate);

            _repository.Save();

            var updatedCourse = _fixture.Context.Courses.Include(c => c.Enrollments).FirstOrDefault(c => c.CourseId == 1);

            updatedCourse.Should().NotBeNull();
            updatedCourse.Enrollments.Should().BeNullOrEmpty();
            _fixture.Context.Enrollments.Any(e => e.CourseId == 1 && e.StudentId == 1).Should().BeFalse();
        }

        [Fact]
        public void ShouldAddAndRemoveEnrollmentToCourseWhenCallingUpdate()
        {
            var courseToUpdate = new CourseEntity
            {
                CourseId = 1,
                Enrollments = new List<EnrollmentEntity>
                {
                    new EnrollmentEntity
                    {
                        CourseId = 1,
                        StudentId = 2
                    }
                }
            };

            _repository.Update(courseToUpdate);

            _repository.Save();

            var updatedCourse = _fixture.Context.Courses.Include(c => c.Enrollments).FirstOrDefault(c => c.CourseId == 1);

            updatedCourse.Should().NotBeNull();
            updatedCourse.Enrollments.All(e => e.CourseId == 1 && e.StudentId == 2).Should().BeTrue();
            _fixture.Context.Enrollments.Any(e => e.CourseId == 1 && e.StudentId != 2).Should().BeFalse();
        }

        [Fact]
        public void ShouldAddCourseAssignmentToCourseWhenCallingUpdate()
        {
            var courseToUpdate = new CourseEntity
            {
                CourseId = 1,
                CourseAssignments = new List<CourseAssignmentEntity>
                {
                    new CourseAssignmentEntity()
                    {
                        CourseId = 1,
                        InstructorId = 1
                    },
                    new CourseAssignmentEntity
                    {
                        CourseId = 1,
                        InstructorId = 2
                    }
                }
            };

            _repository.Update(courseToUpdate);

            _repository.Save();

            var updatedCourse = _fixture.Context.Courses.Include(c => c.CourseAssignments).FirstOrDefault(c => c.CourseId == 1);

            updatedCourse.Should().NotBeNull();
            updatedCourse.CourseAssignments.Count.Should().Be(2);
            updatedCourse.CourseAssignments.Any(ca => ca.InstructorId == 2 && ca.CourseId == 1).Should().BeTrue();
            _fixture.Context.CourseAssignments.Count(ca => ca.InstructorId == 2 && ca.CourseId == 1).Should().Be(1);
        }

        [Fact]
        public void ShouldRemoveCourseAssignmentFromCourseWhenCallingUpdate()
        {
            var courseToUpdate = new CourseEntity
            {
                CourseId = 1,
            };

            _repository.Update(courseToUpdate);

            _repository.Save();

            var updatedCourse = _fixture.Context.Courses.Include(c => c.CourseAssignments).FirstOrDefault(c => c.CourseId == 1);

            updatedCourse.Should().NotBeNull();
            updatedCourse.CourseAssignments.Should().BeNullOrEmpty();
            _fixture.Context.CourseAssignments.Any(ca => ca.CourseId == 1 && ca.InstructorId == 1).Should().BeFalse();
        }

        [Fact]
        public void ShouldAddAndRemoveCourseAssignmentToCourseWhenCallingUpdate()
        {
            var courseToUpdate = new CourseEntity
            {
                CourseId = 1,
                CourseAssignments = new List<CourseAssignmentEntity>
                {
                    new CourseAssignmentEntity
                    {
                        CourseId = 1,
                        InstructorId = 2
                    }
                }
            };

            _repository.Update(courseToUpdate);

            _repository.Save();

            var updatedCourse = _fixture.Context.Courses.Include(c => c.CourseAssignments).FirstOrDefault(c => c.CourseId == 1);

            updatedCourse.Should().NotBeNull();
            updatedCourse.CourseAssignments.All(ca => ca.CourseId == 1 && ca.InstructorId == 2).Should().BeTrue();
            _fixture.Context.CourseAssignments.Any(ca => ca.CourseId == 1 && ca.InstructorId != 2).Should().BeFalse();
        }

        [Fact]
        public void ShouldUpdateDepartmentReferenceWhenCallingUpdate()
        {
            var courseToUpdate = new CourseEntity
            {
                CourseId = 1,
                DepartmentId = 2,
            };

            _repository.Update(courseToUpdate);

            _repository.Save();

            var updatedCourse = _fixture.Context.Courses.Include(c => c.Department).FirstOrDefault(c => c.CourseId == 1);

            updatedCourse.Should().NotBeNull();
            updatedCourse.DepartmentId.Should().Be(2);
            updatedCourse.Department.DepartmentId.Should().Be(2);
            _fixture.Context.Departments.Count().Should().Be(2);
        }
    }
}
