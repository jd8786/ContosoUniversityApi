using System.Collections.Generic;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Services
{
    public interface IEnrollmentsService
    {
        Enrollment Get(int id);

        IEnumerable<Enrollment> FindByStudentId(int studentId);

        Enrollment Add(Enrollment enrollment);

        IEnumerable<Enrollment> AddRange(IEnumerable<Enrollment> enrollments);

        bool Remove(int enrollmentId);

        bool RemoveRange(IEnumerable<Enrollment> enrollments);
    }
}
