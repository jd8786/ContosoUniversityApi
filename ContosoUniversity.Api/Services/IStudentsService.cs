using System.Collections.Generic;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetAll();

        Student Get(int id);

        Student Add(Student student);

        Student Update(Student student);

        bool Remove(int studentId);
    }
}
