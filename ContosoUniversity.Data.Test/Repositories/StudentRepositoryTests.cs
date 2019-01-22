using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories
{
    [Trait("Category", "Unit Test: Data.Repositories.Students")]
    public class StudentRepositoryTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly IStudentsRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public StudentRepositoryTests(InMemoryDbTestFixture fixture)
        {
            _fixture = fixture;

            _fixture.InitData();

            _repository = new StudentsRepository(_fixture.Context);
        }

        public void Dispose()
        {
            _fixture.Dispose();
        }

        [Fact]
        public void ShouldReturnAllTheStudentsWhenCallingGetAll()
        {
            var students = _repository.GetAll().ToList();

            students.Count.Should().Be(2);
        }

        [Fact]
        public void ShouldIncludeAllTheChildrenWhenCallingGetAll()
        {
            var students = _repository.GetAll().ToList();

            students[0].Enrollments.Any(e => e.CourseId == 1 && e.StudentId == 1).Should().BeTrue();
            students[1].Enrollments.Any(e => e.CourseId == 2 && e.StudentId == 2).Should().BeTrue();
        }

        [Fact]
        public void ShouldRemoveStudentWhenCallingRemove()
        {
            var student = new StudentEntity { StudentId = 1 };

            _repository.Remove(student);

            _repository.Save();

            _fixture.Context.Students.Any(s => s.StudentId == 1).Should().BeFalse();
        }

        [Fact]
        public void ShouldRemoveAListOfStudentsWhenCallingRemoveArrange()
        {
            var students = new List<StudentEntity>
            {
                new StudentEntity { StudentId = 1 },
                new StudentEntity { StudentId = 2 }
            };

            _repository.RemoveRange(students);

            _repository.Save();

            _fixture.Context.Students.Any(s => s.StudentId == 1 || s.StudentId == 2).Should().BeFalse();
        }

        [Fact]
        public void ShouldCreateStudentWhenCallingAdd()
        {
            var student = new StudentEntity
            {
                StudentId = 3,
                LastName = "some-last-name"
            };

            _repository.Add(student);

            _repository.Save();

            _fixture.Context.Students.Count(s => s.StudentId == 3).Should().Be(1);
        }

        [Fact]
        public void ShouldCreateAListOfStudentsWhenCallingAddRange()
        {
            var student1 = new StudentEntity
            {
                StudentId = 3,
                LastName = "some-last-name1"
            };

            var student2 = new StudentEntity
            {
                StudentId = 4,
                LastName = "some-last-name2"
            };

            _repository.AddRange(new List<StudentEntity> { student1, student2 });

            _repository.Save();

            _fixture.Context.Students.Count(s => s.StudentId == 3).Should().Be(1);
            _fixture.Context.Students.Count(s => s.StudentId == 4).Should().Be(1);
        }

        [Fact]
        public void ShouldCreateEnrollmentWhenCallingAdd()
        {
            var student = new StudentEntity
            {
                StudentId = 3,
                LastName = "some-last-name",
                Enrollments = new List<EnrollmentEntity>
                {
                    new EnrollmentEntity
                    {
                        CourseId = 1,
                        StudentId = 3,
                    }
                }
            };

            _repository.Add(student);

            _repository.Save();

            var addedStudent = _repository.GetAll().FirstOrDefault(s => s.StudentId == 3);

            addedStudent.Should().NotBeNull();

            addedStudent.Enrollments.Count(e => e.CourseId == 1 && e.StudentId == 3).Should().Be(1);
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
