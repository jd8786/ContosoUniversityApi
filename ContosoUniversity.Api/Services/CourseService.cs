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

        private readonly IStudentValidator _studentValidator;

        private readonly ICourseValidator _courseValidator;

        private readonly IInstructorValidator _instructorValidator;

        private readonly IDepartmentValidator _departmentValidator;

        public CourseService(ICourseRepository courseRepository, IStudentValidator studentValidator, ICourseValidator courseValidator, IInstructorValidator instructorValidator, IDepartmentValidator departmentValidator, IMapper mapper) : base(mapper)
        {
            _courseRepository = courseRepository;

            _studentValidator = studentValidator;

            _courseValidator = courseValidator;

            _instructorValidator = instructorValidator;

            _departmentValidator = departmentValidator;
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

            // toDo: put the following validation in the validator
            if (course.Students != null && course.Students.Any())
            {
                course.Students.ToList().ForEach(s => _studentValidator.ValidateById(s.StudentId));
            }

            if (course.Instructors != null && course.Instructors.Any())
            {
                course.Instructors.ToList().ForEach(i => _instructorValidator.ValidateById(i.InstructorId));
            }

            if (course.Department != null)
            {
                _departmentValidator.ValidateById(course.Department.DepartmentId);
            }

            var courseEntity = MapToEntity(course);

            _courseRepository.Add(courseEntity);

            _courseRepository.Save();

            return Get(courseEntity.CourseId);
        }

        public Course Update(Course course)
        {
            _courseValidator.ValidatePutCourse(course);

            // toDo: put the following validation in the validator
            if (course.Students != null && course.Students.Any())
            {
                course.Students.ToList().ForEach(s => _studentValidator.ValidateById(s.StudentId));
            }

            if (course.Instructors != null && course.Instructors.Any())
            {
                course.Instructors.ToList().ForEach(i => _instructorValidator.ValidateById(i.InstructorId));
            }

            if (course.Department != null)
            {
                _departmentValidator.ValidateById(course.Department.DepartmentId);
            }

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

