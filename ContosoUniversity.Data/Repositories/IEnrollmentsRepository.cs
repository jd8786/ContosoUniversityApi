using System.Collections.Generic;
using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Data.Repositories
{
    public interface IEnrollmentsRepository: IBaseRepository<EnrollmentEntity>
    {
        IEnumerable<EnrollmentEntity> GetByStudentId(int studentId);

        void UpdateEnrollmentGrade(int studentId, int courseId, EnrollmentEntity enrollmentEntity);
    }
}
