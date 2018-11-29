using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Exceptions;

namespace ContosoUniversity.Api.Validators
{
    public class EnrollmentValidator: IEnrollmentValidator
    {
        private readonly IStudentValidator _studentValidator;

        private readonly ICourseValidator _courseValidator;

        public EnrollmentValidator(IStudentValidator studentValidator, ICourseValidator courseValidator)
        {
            _studentValidator = studentValidator;
            _courseValidator = courseValidator;
        }

        public void ValidatePostEnrollment(Enrollment enrollment)
        {
            if (enrollment == null)
            {
                throw new InvalidEnrollmentException("Enrollment must be provided");
            }

            if (enrollment.EnrollmentId != 0)
            {
                throw new InvalidEnrollmentException("Enrollment Id must be 0");
            }

            if (enrollment.StudentId == 0 || enrollment.CourseId == 0)
            {
                throw new InvalidEnrollmentException("Student and course must be provided");
            }

            _studentValidator.Validate(enrollment.StudentId);

            _courseValidator.Validate(enrollment.CourseId);
        }
    }
}
