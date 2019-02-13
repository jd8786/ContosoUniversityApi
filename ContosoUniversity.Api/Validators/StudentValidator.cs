using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Exceptions;

namespace ContosoUniversity.Api.Validators
{
    public class StudentValidator : IStudentValidator
    {
        public ICommonValidator CommonValidator { get; set; }

        public StudentValidator(ICommonValidator commonValidator)
        {
            CommonValidator = commonValidator;
        }

        public void ValidatePostStudent(Student student)
        {
            if (student == null)
            {
                throw new InvalidStudentException("Student must be provided");
            }

            if (student.StudentId != 0)
            {
                throw new InvalidStudentException("Student Id must be 0");
            }

            CommonValidator.ValidateStudentChildren(student);
        }

        public void ValidatePutStudent(Student student)
        {
            if (student == null)
            {
                throw new InvalidStudentException("Student must be provided");
            }

            if (student.StudentId == 0)
            {
                throw new InvalidStudentException("Student Id cannot be 0");
            }

            CommonValidator.IdValidator.ValidateStudentById(student.StudentId);

            CommonValidator.ValidateStudentChildren(student);
        }
    }
}
