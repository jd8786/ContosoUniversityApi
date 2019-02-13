using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Exceptions;

namespace ContosoUniversity.Api.Validators
{
    public class InstructorValidator: IInstructorValidator
    {
        public ICommonValidator CommonValidator { get; set; }

        public InstructorValidator(ICommonValidator commonValidator)
        {
            CommonValidator = commonValidator;
        }

        public void ValidatePostInstructor(Instructor instructor)
        {
            if (instructor == null)
            {
                throw new InvalidInstructorException("Instructor must be provided");
            }

            if (instructor.InstructorId != 0)
            {
                throw new InvalidInstructorException("Instructor Id must be 0");
            }
        }

        public void ValidatePutInstructor(Instructor instructor)
        {
            if (instructor == null)
            {
                throw new InvalidInstructorException("Instructor must be provided");
            }

            if (instructor.InstructorId == 0)
            {
                throw new InvalidInstructorException("Instructor Id cannot be 0");
            }

            CommonValidator.IdValidator.ValidateInstructorById(instructor.InstructorId);
        }
    }
}
