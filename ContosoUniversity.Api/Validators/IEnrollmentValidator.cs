using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public interface IEnrollmentValidator
    {
        void ValidatePostEnrollment(Enrollment enrollment);
    }
}
