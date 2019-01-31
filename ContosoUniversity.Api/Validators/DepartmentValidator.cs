using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using System.Linq;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public class DepartmentValidator : IDepartmentValidator
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentValidator(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public void ValidateById(int departmentId)
        {
            var departments = _departmentRepository.GetAll().ToList();

            var isDepartmentExisting = departments.Any(i => i.DepartmentId == departmentId);

            if (!isDepartmentExisting)
            {
                throw new NotFoundException($"Department provided with Id {departmentId} doesnot exist in the database");
            }
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

            ValidateById(department.DepartmentId);
        }
    }
}
