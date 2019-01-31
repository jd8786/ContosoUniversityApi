using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using System.Linq;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public class InstructorValidator: IInstructorValidator
    {
        private readonly IInstructorRepository _instructorRepository;

        public InstructorValidator(IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        public void ValidateById(int instructorId)
        {
            var instructors = _instructorRepository.GetAll().ToList();

            var isInstructorExisting = instructors.Any(i => i.InstructorId == instructorId);

            if (!isInstructorExisting)
            {
                throw new NotFoundException($"Instructor provided with Id {instructorId} doesnot exist in the database");
            }
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

            ValidateById(instructor.InstructorId);
        }
    }
}
