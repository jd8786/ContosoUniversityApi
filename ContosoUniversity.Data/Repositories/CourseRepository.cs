using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class CourseRepository: BaseRepository<CourseEntity>, ICourseRepository
    {
        public CourseRepository(SchoolContext context) : base(context)
        {
        }

        public override IEnumerable<CourseEntity> GetAll()
        {
            return Context.Courses.AsNoTracking()
                .Include(c => c.Enrollments)
                .ThenInclude(c => c.Student)
                .ToList();
        }
    }
}
