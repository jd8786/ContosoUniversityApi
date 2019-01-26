using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using System.Linq;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public class CourseValidator: ICourseValidator
    {
        private readonly ICourseRepository _coursesRepository;

        public CourseValidator(ICourseRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public void Validate(int courseId)
        {
            var courses = _coursesRepository.GetAll().ToList();

            var isCourseExisting = courses.Any(c => c.CourseId == courseId);

            if (!isCourseExisting)
            {
                throw new NotFoundException($"Course provided with Id {courseId} doesnot exist in the database");
            }
        }

        public void ValidatePostCourse(Course course)
        {
            if (course == null)
            {
                throw new InvalidCourseException("Course must be provided");
            }

            if (course.CourseId != 0)
            {
                throw new InvalidCourseException("Course Id must be 0");
            }
        }

        public void ValidatePutCourse(Course course)
        {
            if (course == null)
            {
                throw new InvalidCourseException("Course must be provided");
            }

            if (course.CourseId == 0)
            {
                throw new InvalidCourseException("Course Id cannot be 0");
            }

            Validate(course.CourseId);
        }
    }
}
