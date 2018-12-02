using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data.EntityModels
{
    [Table("Course")]
    public class CourseEntity
    {
        public CourseEntity()
        {
            CreatedBy = "ContosoUniversityUsers";

            CreatedDate = DateTime.Now;
        }

        [Key]
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

        public int DepartmentId { get; set; }

        public ICollection<EnrollmentEntity> Enrollments { get; set; } = new List<EnrollmentEntity>();

        public ICollection<CourseAssignmentEntity> CourseAssignments { get; set; } = new List<CourseAssignmentEntity>();

        public DepartmentEntity Department { get; set; } = new DepartmentEntity();
    }
}
