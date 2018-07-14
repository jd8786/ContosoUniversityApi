using System.Collections.Generic;
using ContosoUniversity.Data.Models;

namespace ContosoUniversity.Data.Repositories
{
    public interface IStudentsRepository
    {
        IEnumerable<Student> GetStudents();

        Student GetStudentById(int studentId);

        void CreateStudent(Student student);

        bool UpdateStudent(int studentId, Student student);

        bool DeleteStudent(int studentId);
    }
}
