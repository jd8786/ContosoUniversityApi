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
                Title = "new-title",
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

            updatedStudent.Title.Should().Be("new-title");
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

            var updatedCourse = _fixture.Context.Courses.Include(c => c.Enrollments).FirstOrDefault(s => s.CourseId == 1);

            updatedCourse.Should().NotBeNull();

            updatedCourse.Enrollments.Count.Should().Be(2);
            updatedCourse.Enrollments.Any(e => e.StudentId == 2 && e.CourseId == 1).Should().BeTrue();
            _fixture.Context.Enrollments.Count(e => e.StudentId == 2 && e.CourseId == 1).Should().Be(1);
        }

        [Fact]
        public void ShouldRemoveEnrollmentFromStudentWhenCallingUpdate()
        {
            var studentToUpdate = new StudentEntity
            {
                StudentId = 1,
            };

            _repository.Update(studentToUpdate);

            _repository.Save();

            var updatedStudent = _repository.GetAll().FirstOrDefault(s => s.StudentId == 1);

            updatedStudent.Should().NotBeNull();

            updatedStudent.Enrollments.Should().BeNullOrEmpty();
        }

        [Fact]
        public void ShouldAddAndRemoveEnrollmentToStudentWhenCallingUpdate()
        {
            var studentToUpdate = new StudentEntity
            {
                StudentId = 1,
                Enrollments = new List<EnrollmentEntity>
                {
                    new EnrollmentEntity
                    {
                        CourseId = 2,
                        StudentId = 1
                    }
                }
            };

            _repository.Update(studentToUpdate);

            _repository.Save();

            var updatedStudent = _repository.GetAll().FirstOrDefault(s => s.StudentId == 1);

            updatedStudent.Should().NotBeNull();

            updatedStudent.Enrollments.All(e => e.StudentId == 1 && e.CourseId == 2).Should().BeTrue();
        }

        [Fact]
        public void ShouldUpdateGradeWhenCallingUpdate()
        {
            var studentToUpdate = new StudentEntity
            {
                StudentId = 1,
                Enrollments = new List<EnrollmentEntity>
                {
                    new EnrollmentEntity
                    {
                        CourseId = 1,
                        StudentId = 1,
                        Grade = Grade.C
                    }
                }
            };

            _repository.Update(studentToUpdate);

            _repository.Save();

            var updatedStudent = _repository.GetAll().FirstOrDefault(s => s.StudentId == 1);

            updatedStudent.Should().NotBeNull();

            updatedStudent.Enrollments.First(e => e.StudentId == 1 && e.CourseId == 1).Grade.Should().Be(Grade.C);
        }
    }
}
