using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Student
{
    [Trait("Category", "Unit Test: Data.Repositories.Student")]
    public class StudentDeletedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly IStudentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public StudentDeletedTests(InMemoryDbTestFixture fixture)
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
    }
}
