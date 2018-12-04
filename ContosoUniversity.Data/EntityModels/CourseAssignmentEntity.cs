using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data.EntityModels
{
    [Table("CourseAssignment")]
    public class CourseAssignmentEntity
    {
        [Key]
        public int CourseAssignmentId { get; set; }

        public int CourseId { get; set; }

        public int InstructorId { get; set; }

        public virtual CourseEntity Course { get; set; } 

        public virtual InstructorEntity Instructor { get; set; } 
    }
}
