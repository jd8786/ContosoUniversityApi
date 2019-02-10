using AutoMapper;
using ContosoUniversity.Api.AutoMappers;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContosoUniversity.Api.Test.Services
{
    [Trait("Category", "Uint Test: Api.Services.Course")]
    public class CourseServiceTests
    {
        private readonly Mock<ICourseRepository> _courseRepository;

        private readonly Mock<ICourseValidator> _courseValidator;

        private readonly Mock<ICommonValidator> _commonValidator;

        private readonly ICourseService _courseService;

        private readonly List<CourseEntity> _courseEntities;

        public CourseServiceTests()
        {
            _courseRepository = new Mock<ICourseRepository>();
            _courseValidator = new Mock<ICourseValidator>();
            _commonValidator = new Mock<ICommonValidator>();

            _courseValidator.Setup(cv => cv.CommonValidator).Returns(_commonValidator.Object);

            var mapperConfig = new MapperConfiguration(ctg => ctg.AddProfile(new CourseProfile()));

            var mapper = new Mapper(mapperConfig);

            _courseService = new CourseService(_courseRepository.Object, _courseValidator.Object, mapper);

            _courseEntities = new List<CourseEntity>
            {
                new CourseEntity {CourseId = 1},
                new CourseEntity {CourseId = 2}
            };

            _courseRepository.Setup(s => s.GetAll()).Returns(_courseEntities);
        }

        [Fact]
        public void ShouldReturnAllCoursesWhenCallingGetAll()
        {
            var courses = _courseService.GetAll();

            _courseRepository.Verify(cr => cr.GetAll(), Times.Exactly(1));

            courses.Count().Should().Be(2);
        }

        [Fact]
        public void ShouldReturnCourseWhenCallingGetCourseById()
        {
            var course = _courseService.Get(1);

            _commonValidator.Verify(cv => cv.ValidateCourseById(1), Times.Exactly(1));

            _courseRepository.Verify(cr => cr.GetAll(), Times.Exactly(1));

            course.CourseId.Should().Be(1);
        }

        [Fact]
        public void ShouldAddCourseWhenCallingAdd()
        {
            var newCourse = new Course { CourseId = 3 };

            _courseRepository.Setup(cr => cr.Add(It.IsAny<CourseEntity>()))
                .Callback<CourseEntity>(c => _courseEntities.Add(c));

            var addedCourse = _courseService.Add(newCourse);

            _courseValidator.Verify(cv => cv.ValidatePostCourse(newCourse), Times.Exactly(1));

            _courseRepository.Verify(cr => cr.Add(It.Is<CourseEntity>(c => c.CourseId == 3)), Times.Exactly(1));

            _courseRepository.Verify(cr => cr.Save(), Times.Exactly(1));

            addedCourse.CourseId.Should().Be(3);
        }

        [Fact]
        public void ShouldUpdateCourseWhenCallingUpdate()
        {
            var courseToUpdate = new Course { CourseId = 1 };

            var updatedCourse = _courseService.Update(courseToUpdate);

            _courseValidator.Verify(cr => cr.ValidatePutCourse(courseToUpdate), Times.Exactly(1));

            _courseRepository.Verify(cr => cr.Update(It.Is<CourseEntity>(c => c.CourseId == 1)), Times.Exactly(1));

            _courseRepository.Verify(c => c.Save(), Times.Exactly(1));

            updatedCourse.CourseId.Should().Be(1);
        }

        [Fact]
        public void ShouldDeleteCourseWhenCallingDelete()
        {
            _courseRepository.Setup(cr => cr.Find(1)).Returns(new CourseEntity { CourseId = 1 });

            var isRemoved = _courseService.Remove(1);

            _commonValidator.Verify(cv => cv.ValidateCourseById(1), Times.Exactly(1));

            _courseRepository.Verify(cr => cr.Find(1), Times.Exactly(1));

            _courseRepository.Verify(cr => cr.Remove(It.Is<CourseEntity>(c => c.CourseId == 1)), Times.Exactly(1));

            _courseRepository.Verify(cr => cr.Save(), Times.Exactly(1));

            isRemoved.Should().BeTrue();
        }
    }
}
