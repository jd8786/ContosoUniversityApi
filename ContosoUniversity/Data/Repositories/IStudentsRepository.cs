using System.Collections.Generic;
using ContosoUniversity.Data.Models;

namespace ContosoUniversity.Data.Repositories
{
    public interface IStudentsRepository
    {
        IEnumerable<Student> GetStudents();

        Student GetStudentById(int studentId);
    }
}
