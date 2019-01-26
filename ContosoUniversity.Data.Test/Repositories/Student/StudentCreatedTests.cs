using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Student
{
    [Trait("Category", "Unit Test: Data.Repositories.Student")]
    public class StudentCreatedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly IStudentsRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public StudentCreatedTests(InMemoryDbTestFixture fixture)
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

            var addedStudent = _fixture.Context.Students.Include(s => s.Enrollments).FirstOrDefault(s => s.StudentId == 3);

            addedStudent.Should().NotBeNull();

            addedStudent.Enrollments.Count(e => e.CourseId == 1 && e.StudentId == 3).Should().Be(1);
        }
    }
}
