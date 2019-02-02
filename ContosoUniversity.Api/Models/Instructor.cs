using System;
using System.Collections.Generic;

namespace ContosoUniversity.Api.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }

        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        public DateTime HireDate { get; set; }

        public IEnumerable<Course> Courses { get; set; }

        public string OfficeLocation { get; set; }
    }
}
