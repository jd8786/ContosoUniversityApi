using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoUniversity.Data.Models;

namespace ContosoUniversity.Data.Repositories
{
    public interface IStudentsRepository
    {
        Task<IEnumerable<Student>> GetStudentsAsync();

        Task<Student> GetStudentByIdAsync(int studentId);

        Task<Student> CreateAsync(Student student);

        Task<bool> UpdateAsync(Student student);

        Task<bool> DeleteAsync(int studentId);
    }
}
