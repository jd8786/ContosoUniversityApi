using System;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Data.Test.Fixtures;
using FluentAssertions;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Course
{
    [Trait("Category", "Unit Test: Data.Repositories.Student")]
    public class CourseUpdatedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly IStudentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public CourseUpdatedTests(InMemoryDbTestFixture fixture)
        {
            _fixture = fixture;

            _fixture.InitData();

            _repository = new StudentRepository(_fixture.Context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public void ShouldUpdatePrimaryPropertiesWhenCallingUpdate()
        {
            var studentToUpdate = new StudentEntity
            {
                StudentId = 1,
                LastName = "update-last-name",
                FirstMidName = "update-first-mid-name",
                OriginCountry = "update-origin-country",
                EnrollmentDate = new DateTime(2015, 7, 1),
                CreatedBy = "update-user1",
                CreatedDate = new DateTime(2005, 7, 1),
                UpdatedBy = "update-user2",
                UpdatedDate = new DateTime(2010, 7, 1),
            };

            _repository.Update(studentToUpdate);

            _repository.Save();

            var updatedStudent = _fixture.Context.Students.FirstOrDefault(s => s.StudentId == 1);

            updatedStudent.Should().NotBeNull();

            updatedStudent.LastName.Should().Be("update-last-name");
            updatedStudent.FirstMidName.Should().Be("update-first-mid-name");
            updatedStudent.OriginCountry.Should().Be("update-origin-country");
            updatedStudent.EnrollmentDate.Should().Be(new DateTime(2015, 7, 1));
            updatedStudent.CreatedBy.Should().Be("update-user1");
            updatedStudent.CreatedDate.Should().Be(new DateTime(2005, 7, 1));
            updatedStudent.UpdatedBy.Should().Be("update-user2");
            updatedStudent.UpdatedDate.Should().Be(new DateTime(2010, 7, 1));
        }

        [Fact]
        public void ShouldAddEnrollmentToStudentWhenCallingUpdate()
        {
            var studentToUpdate = new StudentEntity
            {
                StudentId = 1,
                Enrollments = new List<EnrollmentEntity>
                {
                    new EnrollmentEntity
                    {
                        CourseId = 1,
                        StudentId = 1
                    },
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

            updatedStudent.Enrollments.Count.Should().Be(2);
            updatedStudent.Enrollments.Any(e => e.StudentId == 1 && e.CourseId == 2).Should().BeTrue();
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
