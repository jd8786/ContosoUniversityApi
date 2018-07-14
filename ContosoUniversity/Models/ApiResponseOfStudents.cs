using System.Collections.Generic;
using ContosoUniversity.Data.Models;

namespace ContosoUniversity.Models
{
    public class ApiResponseOfStudents: ApiResponse<IEnumerable<Student>>
    {
    }
}
