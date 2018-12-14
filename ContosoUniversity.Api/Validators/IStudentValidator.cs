using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public interface IStudentValidator
    {
        void Validate(int id);

        void ValidatePostStudent(Student student);

        void ValidatePutStudent(Student student);
    }
}
