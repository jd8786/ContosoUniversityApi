using System;
using System.Collections.Generic;

namespace ContosoUniversity.Api.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }

        public Instructor Administrator { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}
