using System.Collections.Generic;
using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Data.Repositories
{
    public interface IEnrollmentRepository: IBaseRepository<EnrollmentEntity>
    {
        IEnumerable<EnrollmentEntity> GetByStudentId(int studentId);
    }
}
