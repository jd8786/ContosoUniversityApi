using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Data.Repositories
{
    public class CoursesRepository: BaseRepository<CourseEntity>, ICoursesRepository
    {
        public CoursesRepository(SchoolContext context) : base(context)
        {
        }
    }
}
