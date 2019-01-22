using System;
using System.Collections.Generic;

namespace ContosoUniversity.Api.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public string OriginCountry { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}
