using System.Collections.Generic;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudents();

        Student GetStudentById(int id);

        Student AddStudent(Student student);

        Student UpdateStudent(Student student);

        bool RemoveStudent(int studentId);
    }
}
