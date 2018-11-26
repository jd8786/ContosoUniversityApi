using System.Linq;
using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Data.Repositories
{
    public class EnrollmentsRepository: BaseRepository<EnrollmentEntity>, IEnrollmentsRepository
    {
        public EnrollmentsRepository(SchoolContext context) : base(context)
        {
        }
    }
}
