using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using System.Linq;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public class OfficeAssignmentValidator : IOfficeAssignmentValidator
    {
        private readonly IOfficeAssignmentRepository _officeAssignmentRepository;

        public OfficeAssignmentValidator(IOfficeAssignmentRepository officeAssignmentRepository)
        {
            _officeAssignmentRepository = officeAssignmentRepository;
        }

        public void ValidateById(int instructorId)
        {
            var officeAssignments = _officeAssignmentRepository.GetAll().ToList();

            var isOfficeAssignmentExisting = officeAssignments.Any(i => i.InstructorId == instructorId);

            if (!isOfficeAssignmentExisting)
            {
                throw new NotFoundException($"OfficeAssignment provided with Id {instructorId} doesnot exist in the database");
            }
        }

        public void ValidatePostOfficeAssignment(OfficeAssignment officeAssignment)
        {
            if (officeAssignment == null)
            {
                throw new InvalidOfficeAssignmentException("OfficeAssignment must be provided");
            }

            if (officeAssignment.InstructorId != 0)
            {
                throw new InvalidDepartmentException("Department Id must be 0");
            }
        }

        public void ValidatePutOfficeAssignment(OfficeAssignment officeAssignment)
        {
            if (officeAssignment == null)
            {
                throw new InvalidOfficeAssignmentException("OfficeAssignment must be provided");
            }

            if (officeAssignment.InstructorId == 0)
            {
                throw new InvalidDepartmentException("OfficeAssignment Id cannot be 0");
            }

            ValidateById(officeAssignment.InstructorId);
        }
    }
}
