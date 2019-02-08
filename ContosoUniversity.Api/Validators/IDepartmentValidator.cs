using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public interface IDepartmentValidator
    {
        ICommonValidator CommonValidator { get; set; }

        void ValidatePostDepartment(Department department);

        void ValidatePutDepartment(Department department);
    }
}
