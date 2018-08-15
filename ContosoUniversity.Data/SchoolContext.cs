using ContosoUniversity.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    public class SchoolContext: DbContext
    {
        public SchoolContext(DbContextOptions options): base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Course> Courses  { get; set; }

        public virtual DbSet<Enrollment> Enrollments { get; set; }
    }
}
