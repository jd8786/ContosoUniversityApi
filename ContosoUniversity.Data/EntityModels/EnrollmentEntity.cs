using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data.EntityModels
{
    [Table("Enrollment")]
    public class EnrollmentEntity
    {
        [Key]
        public int EnrollmentId { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }

        public Grade? Grade { get; set; }

        public StudentEntity Student { get; set; } = new StudentEntity();

        public CourseEntity Course { get; set; } = new CourseEntity();
    }
}
