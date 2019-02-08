using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public interface IInstructorValidator
    {
        ICommonValidator CommonValidator { get; set; }

        void ValidatePostInstructor(Instructor instructor);

        void ValidatePutInstructor(Instructor instructor);
    }
}
