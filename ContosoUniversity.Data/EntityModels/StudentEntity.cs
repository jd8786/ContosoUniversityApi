using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Data.EntityModels
{
    [Table("Student")]
    public class StudentEntity
    {
        public StudentEntity()
        {
            Enrollments = new List<EnrollmentEntity>();

            CreatedBy = "ContosoUniversityUsers";

            CreatedDate = DateTime.Now;
        }

        [Key]
        public int StudentId { get; set; }

        [Required, MaxLength(30)]
        public string LastName { get; set; }

        [Required, MaxLength(30)]
        public string FirstMidName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        [Required, MaxLength(50)]
        public string OriginCountry { get; set; }

        [Required, MaxLength(30)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<EnrollmentEntity> Enrollments { get; set; }
    }
}
