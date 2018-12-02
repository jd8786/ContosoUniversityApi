using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class StudentsRepository: BaseRepository<StudentEntity>, IStudentsRepository
    {
        public StudentsRepository(SchoolContext context) : base(context)
        {
        }

        public override IEnumerable<StudentEntity> GetAll()
        {
            return Context.Students.AsNoTracking()
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToList();
        }
    }
}
