using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data.EntityModels
{
    [Table("Department")]
    public class DepartmentEntity
    {
        public DepartmentEntity()
        {
            CreatedBy = "ContosoUniversityUsers";

            CreatedDate = DateTime.Now;
        }

        [Key]
        public int DepartmentId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        public int? InstructorId { get; set; }

        [Required, MaxLength(30)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public InstructorEntity Administrator { get; set; } = new InstructorEntity();

        public ICollection<CourseEntity> Courses { get; set; } = new List<CourseEntity>();
    }
}
