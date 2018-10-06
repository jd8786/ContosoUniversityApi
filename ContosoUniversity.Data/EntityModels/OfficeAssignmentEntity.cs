using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data.EntityModels
{
    [Table("OfficeAssignment")]
    public class OfficeAssignmentEntity
    {
        [Key]
        public int OfficeAssignmentId { get; set; }

        [Required, MaxLength(50)]
        public string Location { get; set; }

        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        public virtual InstructorEntity Instructor { get; set; }
    }
}
