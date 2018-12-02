using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class EnrollmentsRepository: BaseRepository<EnrollmentEntity>, IEnrollmentsRepository
    {
        public EnrollmentsRepository(SchoolContext context) : base(context)
        {
        }

        public override IEnumerable<EnrollmentEntity> GetAll()
        {
            return Context.Enrollments.AsNoTracking()
                .Include(e => e.Course)
                .Include(e => e.Student)
                .ToList();
        }
    }
}
