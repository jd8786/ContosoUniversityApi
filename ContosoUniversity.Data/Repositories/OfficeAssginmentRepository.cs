using System.Collections.Generic;
using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class OfficeAssignmentRepository : BaseRepository<OfficeAssignmentEntity>, IOfficeAssignmentRepository
    {
        public OfficeAssignmentRepository(SchoolContext context) : base(context)
        {
        }

        public override IEnumerable<OfficeAssignmentEntity> GetAll()
        {
            return Context.OfficeAssignments
                .Include(o => o.Instructor);
        }
    }
}
