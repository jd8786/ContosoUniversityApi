using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Data.Repositories
{
    public class StudentsRepository: BaseRepository<StudentEntity>, IStudentsRepository
    {
        public StudentsRepository(SchoolContext context) : base(context)
        {
        }
    }
}
