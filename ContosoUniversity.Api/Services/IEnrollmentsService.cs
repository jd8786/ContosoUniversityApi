using System.Collections.Generic;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Services
{
    public interface IEnrollmentsService
    {
        IEnumerable<Enrollment> AddRange(IEnumerable<Enrollment> enrollments);

        IEnumerable<Enrollment> Update(int studentId, IEnumerable<Enrollment> enrollments);
    }
}
