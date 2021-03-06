﻿using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using System;
using System.Linq;
using ContosoUniversity.Data.Test.Fixtures;
using Xunit;

namespace ContosoUniversity.Data.Test.Repositories.Student
{
    [Trait("Category", "Unit Test: Data.Repositories.Student")]
    public class StudentRetrievedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly StudentRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public StudentRetrievedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldReturnAllStudentsWhenCallingGetAll()
        {
            var students = _repository.GetAll().ToList();

            students.Count.Should().Be(2);
        }

        [Fact]
        public void ShouldIncludeEnrollmentsWhenCallingGetAll()
        {
            var students = _repository.GetAll().ToList();

            students[0].Enrollments.Any(e => e.Course.CourseId == 1 && e.StudentId == 1).Should().BeTrue();
            students[1].Enrollments.Any(e => e.Course.CourseId == 2 && e.StudentId == 2).Should().BeTrue();
        }
    }
}
