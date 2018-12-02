using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data.EntityModels
{
    [Table("Instructor")]
    public class InstructorEntity
    {
        public InstructorEntity()
        {
            CreatedBy = "ContosoUniversityUsers";

            CreatedDate = DateTime.Now;
        }

        [Key]
        public int InstructorId { get; set; }

        [Required, MaxLength(30)]
        public string LastName { get; set; }

        [Required, MaxLength(30)]
        public string FirstMidName { get; set; }

        public DateTime HireDate { get; set; }

        [Required, MaxLength(30)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public ICollection<CourseAssignmentEntity> CourseAssignments { get; set; } = new List<CourseAssignmentEntity>();

        public OfficeAssignmentEntity OfficeAssignment { get; set; } = new OfficeAssignmentEntity();
    }
}
