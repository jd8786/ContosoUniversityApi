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

        public CourseEntity Course { get; set; } = new CourseEntity();

        public InstructorEntity Instructor { get; set; } = new InstructorEntity();
    }
}
