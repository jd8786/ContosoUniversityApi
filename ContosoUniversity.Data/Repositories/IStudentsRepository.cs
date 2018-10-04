using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Data.Repositories
{
    public interface IStudentsRepository
    {
        Task<IEnumerable<StudentEntity>> GetStudentsAsync();

        Task<StudentEntity> GetStudentByIdAsync(int studentId);

        Task<StudentEntity> CreateAsync(StudentEntity student);

        Task<bool> UpdateAsync(StudentEntity student);

        Task<bool> DeleteAsync(int studentId);
    }
}
