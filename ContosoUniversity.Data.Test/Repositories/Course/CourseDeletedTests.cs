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
    [Trait("Category", "Unit Test: Data.Repositories.Course")]
    public class CourseDeletedTests : IClassFixture<InMemoryDbTestFixture>, IDisposable
    {
        private readonly ICourseRepository _repository;
        private readonly InMemoryDbTestFixture _fixture;

        public CourseDeletedTests(InMemoryDbTestFixture fixture)
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
        public void ShouldRemoveCourseWhenCallingRemove()
        {
            var course = new CourseEntity { CourseId = 1 };

            _repository.Remove(course);

            _repository.Save();

            _fixture.Context.Courses.Any(s => s.CourseId == 1).Should().BeFalse();
        }

        [Fact]
        public void ShouldRemoveAListOfCoursesWhenCallingRemoveArrange()
        {
            var courses = new List<CourseEntity>
            {
                new CourseEntity { CourseId = 1 },
                new CourseEntity { CourseId = 2 }
            };

            _repository.RemoveRange(courses);

            _repository.Save();

            _fixture.Context.Courses.Any(s => s.CourseId == 1 || s.CourseId == 2).Should().BeFalse();
        }
    }
}
