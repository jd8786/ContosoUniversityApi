using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Exceptions;

namespace ContosoUniversity.Api.Validators
{
    public class DepartmentValidator : IDepartmentValidator
    {
        public ICommonValidator CommonValidator { get; set; }

        public DepartmentValidator(ICommonValidator commonValidator)
        {
            CommonValidator = commonValidator;
        }

        public void ValidatePostDepartment(Department department)
        {
            if (department == null)
            {
                throw new InvalidDepartmentException("Department must be provided");
            }

            if (department.DepartmentId != 0)
            {
                throw new InvalidDepartmentException("Department Id must be 0");
            }
        }

        public void ValidatePutDepartment(Department department)
        {
            if (department == null)
            {
                throw new InvalidDepartmentException("Department must be provided");
            }

            if (department.DepartmentId == 0)
            {
                throw new InvalidDepartmentException("Department Id cannot be 0");
            }

            CommonValidator.ValidateDepartmentById(department.DepartmentId);
        }
    }
}
