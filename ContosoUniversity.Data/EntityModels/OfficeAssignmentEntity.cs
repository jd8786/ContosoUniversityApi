using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data.EntityModels
{
    [Table("OfficeAssignment")]
    public class OfficeAssignmentEntity
    {
        [Key]
        public int InstructorId { get; set; }

        [Required, MaxLength(50)]
        public string Location { get; set; }

        public InstructorEntity Instructor { get; set; } = new InstructorEntity();
    }
}
