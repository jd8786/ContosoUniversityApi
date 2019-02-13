using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Exceptions;

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

            CommonValidator.ValidateCourseChildren(course);
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

            CommonValidator.IdValidator.ValidateCourseById(course.CourseId);

            CommonValidator.ValidateCourseChildren(course);
        }
    }
}
