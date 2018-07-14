using System.Collections.Generic;
using ContosoUniversity.Data.Models;

namespace ContosoUniversity.Models
{
    public class ApiResponseOfStudents: ApiResponseWithData<IEnumerable<Student>>
    {
    }
}
