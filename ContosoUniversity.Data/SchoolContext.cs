using System;
using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    public class SchoolContext: DbContext
    {
        public SchoolContext(DbContextOptions options): base(options)
        {
        }

        public virtual DbSet<StudentEntity> Students { get; set; }

        public virtual DbSet<CourseEntity> Courses  { get; set; }

        public virtual DbSet<EnrollmentEntity> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<StudentEntity>().HasData(
            //    new StudentEntity { StudentId = 1, FirstMidName = "Carson", LastName = "Alexander", OriginCountry = "USA", EnrollmentDate = DateTime.Parse("2005-09-01") },
            //    new StudentEntity { StudentId = 2, FirstMidName = "Meredith", LastName = "Alonso", OriginCountry = "CHINA", EnrollmentDate = DateTime.Parse("2002-09-01") },
            //    new StudentEntity { StudentId = 3, FirstMidName = "Arturo", LastName = "Anand", OriginCountry = "USA", EnrollmentDate = DateTime.Parse("2003-09-01") },
            //    new StudentEntity { StudentId = 4, FirstMidName = "Gytis", LastName = "Barzdukas", OriginCountry = "JAPAN", EnrollmentDate = DateTime.Parse("2002-09-01") },
            //    new StudentEntity { StudentId = 5, FirstMidName = "Yan", LastName = "Li", OriginCountry = "USA", EnrollmentDate = DateTime.Parse("2002-09-01") },
            //    new StudentEntity { StudentId = 6, FirstMidName = "Peggy", LastName = "Justice", OriginCountry = "ENGLAND", EnrollmentDate = DateTime.Parse("2001-09-01") },
            //    new StudentEntity { StudentId = 7, FirstMidName = "Laura", LastName = "Norman", OriginCountry = "FRANCE", EnrollmentDate = DateTime.Parse("2003-09-01") },
            //    new StudentEntity { StudentId = 8, FirstMidName = "Nino", LastName = "Olivetto", OriginCountry = "GERMANY", EnrollmentDate = DateTime.Parse("2005-09-01") }
            //);

            //builder.Entity<CourseEntity>().HasData(
            //    new CourseEntity { CourseId = 1050, Title = "Chemistry", Credits = 3 },
            //    new CourseEntity { CourseId = 4022, Title = "Microeconomics", Credits = 3 },
            //    new CourseEntity { CourseId = 4041, Title = "Macroeconomics", Credits = 3 },
            //    new CourseEntity { CourseId = 1045, Title = "Calculus", Credits = 4 },
            //    new CourseEntity { CourseId = 3141, Title = "Trigonometry", Credits = 4 },
            //    new CourseEntity { CourseId = 2021, Title = "Composition", Credits = 3 },
            //    new CourseEntity { CourseId = 2042, Title = "Literature", Credits = 4 }
            //);

            //builder.Entity<EnrollmentEntity>().HasData(
            //    new EnrollmentEntity { EnrollmentId = 1, StudentId = 1, CourseId = 1050, Grade = Grade.A },
            //    new EnrollmentEntity { EnrollmentId = 2, StudentId = 1, CourseId = 4022, Grade = Grade.C },
            //    new EnrollmentEntity { EnrollmentId = 3, StudentId = 1, CourseId = 4041, Grade = Grade.B },
            //    new EnrollmentEntity { EnrollmentId = 4, StudentId = 2, CourseId = 1045, Grade = Grade.B },
            //    new EnrollmentEntity { EnrollmentId = 5, StudentId = 2, CourseId = 3141, Grade = Grade.F },
            //    new EnrollmentEntity { EnrollmentId = 6, StudentId = 2, CourseId = 2021, Grade = Grade.F },
            //    new EnrollmentEntity { EnrollmentId = 7, StudentId = 3, CourseId = 1050 },
            //    new EnrollmentEntity { EnrollmentId = 8, StudentId = 4, CourseId = 1050 },
            //    new EnrollmentEntity { EnrollmentId = 9, StudentId = 4, CourseId = 4022, Grade = Grade.F },
            //    new EnrollmentEntity { EnrollmentId = 10, StudentId = 5, CourseId = 4041, Grade = Grade.C },
            //    new EnrollmentEntity { EnrollmentId = 11, StudentId = 6, CourseId = 1045 },
            //    new EnrollmentEntity { EnrollmentId = 12, StudentId = 7, CourseId = 3141, Grade = Grade.A }
            //);
        }
    }
}
