using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data.Models
{
    [Table("Course")]
    public class Course
    {
        public Course()
        {
            Enrollments = new List<Enrollment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseId { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        public int Credits { get; set; }

        [Required, MaxLength(30)]
        public string CreatedBy { get; set; } 
        
        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
