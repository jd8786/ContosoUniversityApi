using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public interface IInstructorValidator
    {
        void ValidateById(int instructorId);

        void ValidatePostInstructor(Instructor instructor);

        void ValidatePutInstructor(Instructor instructor);
    }
}
