using ContosoUniversity.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    public class SchoolContext: DbContext
    {
        public SchoolContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses  { get; set; }

        public DbSet<Enrollment> Enrollments { get; set; }
    }
}
