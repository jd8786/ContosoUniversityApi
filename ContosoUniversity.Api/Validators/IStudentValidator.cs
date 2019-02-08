using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public interface IStudentValidator
    {
        ICommonValidator CommonValidator { get; set; }

        void ValidatePostStudent(Student student);

        void ValidatePutStudent(Student student);
    }
}
