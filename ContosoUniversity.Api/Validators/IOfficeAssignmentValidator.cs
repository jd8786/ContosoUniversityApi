using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public interface IOfficeAssignmentValidator
    {
        void ValidateById(int instructorId);

        void ValidatePostOfficeAssignment(OfficeAssignment officeAssignment);

        void ValidatePutOfficeAssignment(OfficeAssignment officeAssignment);
    }
}
