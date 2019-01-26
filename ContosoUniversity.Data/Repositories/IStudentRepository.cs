using System.Linq;
using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Data.Repositories
{
    public interface IStudentRepository: IBaseRepository<StudentEntity>
    {
    }
}
