using System.Collections.Generic;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Services
{
    public interface IStudentService
    {
        List<StudentInfo> GetStudents();

        StudentInfo GetStudentById(int id);
    }
}
