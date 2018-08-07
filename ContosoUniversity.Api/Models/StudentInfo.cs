using System;
using System.Collections.Generic;

namespace ContosoUniversity.Api.Models
{
    public class StudentInfo
    {
        public int StudentInfoId { get; set; }

        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public List<CourseInfo> CourseInfos { get; set; }
    }
}
