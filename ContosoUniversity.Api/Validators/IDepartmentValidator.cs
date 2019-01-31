using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public interface IDepartmentValidator
    {
        void ValidateById(int departmentId);

        void ValidatePostDepartment(Department department);

        void ValidatePutDepartment(Department department);
    }
}
