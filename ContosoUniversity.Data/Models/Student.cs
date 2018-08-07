using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data.Models
{
    [Table("Student")]
    public class Student
    {
        public int StudentId { get; set; }

        [Required, MaxLength(30)]
        public string LastName { get; set; }

        [Required, MaxLength(30)]
        public string FirstMidName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        [Required, MaxLength(30)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
