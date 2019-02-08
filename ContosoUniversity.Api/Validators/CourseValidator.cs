using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Exceptions;
using System.Linq;

namespace ContosoUniversity.Api.Validators
{
    public class CourseValidator : ICourseValidator
    {
        public ICommonValidator CommonValidator { get; set; }

        public CourseValidator(ICommonValidator commonValidator)
        {
            CommonValidator = commonValidator;
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

            CommonValidator.ValidateCourseById(course.CourseId);

            ValidateChildren(course);
        }

        private void ValidateChildren(Course course)
        {
            if (course.Students != null && course.Students.Any())
            {
                course.Students.ToList().ForEach(s => CommonValidator.ValidateStudentById(s.StudentId));
            }

            if (course.Instructors != null && course.Instructors.Any())
            {
                course.Instructors.ToList().ForEach(i => CommonValidator.ValidateInstructorById(i.InstructorId));
            }

            if (course.Department != null)
            {
                CommonValidator.ValidateDepartmentById(course.Department.DepartmentId);
            }
        }
    }
}
