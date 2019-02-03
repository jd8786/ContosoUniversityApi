using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Api.Services
{
    public class CourseService : MapperService<CourseEntity, Course>, ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        private readonly ICourseValidator _courseValidator;

        public CourseService(ICourseRepository courseRepository, ICourseValidator courseValidator, IMapper mapper) : base(mapper)
        {
            _courseRepository = courseRepository;

            _courseValidator = courseValidator;
        }

        public IEnumerable<Course> GetAll()
        {
            var courseEntities = _courseRepository.GetAll();

            return MapToModels(courseEntities);
        }

        public Course Get(int id)
        {
            _courseValidator.ValidateById(id);

            var courseEntity = _courseRepository.GetAll().First(s => s.CourseId == id);

            return MapToModel(courseEntity);
        }

        public Course Add(Course course)
        {
            _courseValidator.ValidatePostCourse(course);

            var courseEntity = MapToEntity(course);

            _courseRepository.Add(courseEntity);

            _courseRepository.Save();

            return Get(courseEntity.CourseId);
        }

        public Course Update(Course course)
        {
            _courseValidator.ValidatePutCourse(course);

            var courseEntity = MapToEntity(course);

            _courseRepository.Update(courseEntity);

            _courseRepository.Save();

            return Get(courseEntity.CourseId);
        }

        public bool Remove(int courseId)
        {
            _courseValidator.ValidateById(courseId);

            var courseEntity = _courseRepository.GetAll().First(c => c.CourseId == courseId);

            _courseRepository.Remove(courseEntity);

            _courseRepository.Save();

            return true;
        }
    }
}

