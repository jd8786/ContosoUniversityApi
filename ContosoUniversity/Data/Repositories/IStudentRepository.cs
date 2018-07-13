using System.Collections.Generic;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data.Repositories
{
    public interface IStudentRepository
    {
        IEnumerable<Student> GetStudents();

        Student GetStudentById(int studentId);
    }
}
