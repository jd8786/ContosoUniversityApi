using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data.Models
{
    [Table("Enrollment")]
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }

        public Grade? Grade { get; set; }

        public Student Student { get; set; }

        public Course Course{ get; set; }
    }
}
