using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using System.Linq;

namespace ContosoUniversity.Api.Validators
{
    public class CourseValidator: ICourseValidator
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentValidator _studentValidator;
        private readonly IInstructorValidator _instructorValidator;
        private readonly IDepartmentValidator _departmentValidator;

        public CourseValidator(ICourseRepository courseRepository, IStudentValidator studentValidator, IInstructorValidator instructorValidator, IDepartmentValidator departmentValidator )
        {
            _courseRepository = courseRepository;
            _studentValidator = studentValidator;
            _instructorValidator = instructorValidator;
            _departmentValidator = departmentValidator;
        }

        public void ValidateById(int courseId)
        {
            var courses = _courseRepository.GetAll().ToList();

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

            ValidateChildren(course);
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

            ValidateById(course.CourseId);

            ValidateChildren(course);
        }

        private void ValidateChildren(Course course)
        {
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
        }
    }
}
