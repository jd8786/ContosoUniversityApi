using System;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<StudentEntity> Students { get; set; }

        public virtual DbSet<CourseEntity> Courses { get; set; }

        public virtual DbSet<EnrollmentEntity> Enrollments { get; set; }

        public virtual DbSet<InstructorEntity> Instructors { get; set; }

        public virtual DbSet<DepartmentEntity> Departments { get; set; }

        public virtual DbSet<OfficeAssignmentEntity> OfficeAssignments { get; set; }

        public virtual DbSet<CourseAssignmentEntity> CourseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var students = new StudentEntity[]
            {
                new StudentEntity { StudentId = 1, FirstMidName = "Carson",  LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01"), OriginCountry = "USA" },
                new StudentEntity { StudentId = 2, FirstMidName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01"), OriginCountry = "UK" },
                new StudentEntity { StudentId = 3, FirstMidName = "Arturo", LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01"), OriginCountry = "TURKEY" },
                new StudentEntity { StudentId = 4, FirstMidName = "Gytis", LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01"), OriginCountry = "CHINA" },
                new StudentEntity { StudentId = 5, FirstMidName = "Yan", LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01"), OriginCountry = "JAPAN" },
                new StudentEntity { StudentId = 6, FirstMidName = "Peggy", LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01"), OriginCountry = "UK" },
                new StudentEntity { StudentId = 7, FirstMidName = "Laura", LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01"), OriginCountry = "USA" },
                new StudentEntity { StudentId = 8, FirstMidName = "Nino", LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01"), OriginCountry = "RUSSIA"}
            };

            var instructors = new InstructorEntity[]
            {
                new InstructorEntity { InstructorId = 1, FirstMidName = "Kim", LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new InstructorEntity { InstructorId = 2, FirstMidName = "Fadi", LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new InstructorEntity { InstructorId = 3, FirstMidName = "Roger", LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new InstructorEntity { InstructorId = 4, FirstMidName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new InstructorEntity { InstructorId = 5, FirstMidName = "Roger", LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };

            var departments = new DepartmentEntity[]
            {
                new DepartmentEntity { DepartmentId = 1, Name = "English", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorId = instructors.Single( i => i.LastName == "Abercrombie").InstructorId },
                new DepartmentEntity { DepartmentId = 2, Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorId = instructors.Single( i => i.LastName == "Fakhouri").InstructorId },
                new DepartmentEntity { DepartmentId = 3, Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorId = instructors.Single( i => i.LastName == "Harui").InstructorId },
                new DepartmentEntity { DepartmentId = 4, Name = "Economics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorId = instructors.Single( i => i.LastName == "Kapoor").InstructorId }
            };

            var courses = new CourseEntity[]
            {
                new CourseEntity {CourseId = 1050, Title = "Chemistry", Credits = 3,
                    DepartmentId = departments.Single( s => s.Name == "Engineering").DepartmentId
                },
                new CourseEntity {CourseId = 4022, Title = "Microeconomics", Credits = 3,
                    DepartmentId = departments.Single( s => s.Name == "Economics").DepartmentId
                },
                new CourseEntity {CourseId = 4041, Title = "Macroeconomics", Credits = 3,
                    DepartmentId = departments.Single( s => s.Name == "Economics").DepartmentId
                },
                new CourseEntity {CourseId = 1045, Title = "Calculus", Credits = 4,
                    DepartmentId = departments.Single( s => s.Name == "Mathematics").DepartmentId
                },
                new CourseEntity {CourseId = 3141, Title = "Trigonometry", Credits = 4,
                    DepartmentId = departments.Single( s => s.Name == "Mathematics").DepartmentId
                },
                new CourseEntity {CourseId = 2021, Title = "Composition", Credits = 3,
                    DepartmentId = departments.Single( s => s.Name == "English").DepartmentId
                },
                new CourseEntity {CourseId = 2042, Title = "Literature", Credits = 4,
                    DepartmentId = departments.Single( s => s.Name == "English").DepartmentId
                },
            };

            var officeAssignments = new OfficeAssignmentEntity[]
            {
                new OfficeAssignmentEntity {
                    InstructorId = instructors.Single( i => i.LastName == "Fakhouri").InstructorId,
                    Location = "Smith 17" },
                new OfficeAssignmentEntity {
                    InstructorId = instructors.Single( i => i.LastName == "Harui").InstructorId,
                    Location = "Gowan 27" },
                new OfficeAssignmentEntity {
                    InstructorId = instructors.Single( i => i.LastName == "Kapoor").InstructorId,
                    Location = "Thompson 304" },
            };

            var courseInstructors = new CourseAssignmentEntity[]
            {
                new CourseAssignmentEntity {
                    CourseAssignmentId = 1,
                    CourseId = courses.Single(c => c.Title == "Chemistry" ).CourseId,
                    InstructorId = instructors.Single(i => i.LastName == "Kapoor").InstructorId
                    },
                new CourseAssignmentEntity {
                    CourseAssignmentId = 2,
                    CourseId = courses.Single(c => c.Title == "Chemistry" ).CourseId,
                    InstructorId = instructors.Single(i => i.LastName == "Harui").InstructorId
                    },
                new CourseAssignmentEntity {
                    CourseAssignmentId = 3,
                    CourseId = courses.Single(c => c.Title == "Microeconomics" ).CourseId,
                    InstructorId = instructors.Single(i => i.LastName == "Zheng").InstructorId
                    },
                new CourseAssignmentEntity {
                    CourseAssignmentId = 4,
                    CourseId = courses.Single(c => c.Title == "Macroeconomics" ).CourseId,
                    InstructorId = instructors.Single(i => i.LastName == "Zheng").InstructorId
                    },
                new CourseAssignmentEntity {
                    CourseAssignmentId = 5,
                    CourseId = courses.Single(c => c.Title == "Calculus" ).CourseId,
                    InstructorId = instructors.Single(i => i.LastName == "Fakhouri").InstructorId
                    },
                new CourseAssignmentEntity {
                    CourseAssignmentId = 6,
                    CourseId = courses.Single(c => c.Title == "Trigonometry" ).CourseId,
                    InstructorId = instructors.Single(i => i.LastName == "Harui").InstructorId
                    },
                new CourseAssignmentEntity {
                    CourseAssignmentId = 7,
                    CourseId = courses.Single(c => c.Title == "Composition" ).CourseId,
                    InstructorId = instructors.Single(i => i.LastName == "Abercrombie").InstructorId
                    },
                new CourseAssignmentEntity {
                    CourseAssignmentId = 8,
                    CourseId = courses.Single(c => c.Title == "Literature" ).CourseId,
                    InstructorId = instructors.Single(i => i.LastName == "Abercrombie").InstructorId
                    },
            };

            var enrollments = new EnrollmentEntity[]
            {
                new EnrollmentEntity {
                    EnrollmentId = 1,
                    StudentId = students.Single(s => s.LastName == "Alexander").StudentId,
                    CourseId = courses.Single(c => c.Title == "Chemistry" ).CourseId,
                    Grade = Grade.A
                },
                new EnrollmentEntity {
                    EnrollmentId = 2,
                    StudentId = students.Single(s => s.LastName == "Alexander").StudentId,
                    CourseId = courses.Single(c => c.Title == "Microeconomics" ).CourseId,
                    Grade = Grade.C
                    },
                new EnrollmentEntity {
                    EnrollmentId = 3,
                    StudentId = students.Single(s => s.LastName == "Alexander").StudentId,
                    CourseId = courses.Single(c => c.Title == "Macroeconomics" ).CourseId,
                    Grade = Grade.B
                    },
                new EnrollmentEntity {
                    EnrollmentId = 4,
                    StudentId = students.Single(s => s.LastName == "Alonso").StudentId,
                    CourseId = courses.Single(c => c.Title == "Calculus" ).CourseId,
                    Grade = Grade.B
                    },
                new EnrollmentEntity {
                    EnrollmentId = 5,
                    StudentId = students.Single(s => s.LastName == "Alonso").StudentId,
                    CourseId = courses.Single(c => c.Title == "Trigonometry" ).CourseId,
                    Grade = Grade.B
                    },
                new EnrollmentEntity {
                    EnrollmentId = 6,
                    StudentId = students.Single(s => s.LastName == "Alonso").StudentId,
                    CourseId = courses.Single(c => c.Title == "Composition" ).CourseId,
                    Grade = Grade.B
                    },
                new EnrollmentEntity {
                    EnrollmentId = 7,
                    StudentId = students.Single(s => s.LastName == "Anand").StudentId,
                    CourseId = courses.Single(c => c.Title == "Chemistry" ).CourseId
                    },
                new EnrollmentEntity {
                    EnrollmentId = 8,
                    StudentId = students.Single(s => s.LastName == "Anand").StudentId,
                    CourseId = courses.Single(c => c.Title == "Microeconomics").CourseId,
                    Grade = Grade.B
                    },
                new EnrollmentEntity {
                    EnrollmentId = 9,
                    StudentId = students.Single(s => s.LastName == "Barzdukas").StudentId,
                    CourseId = courses.Single(c => c.Title == "Chemistry").CourseId,
                    Grade = Grade.B
                    },
                new EnrollmentEntity {
                    EnrollmentId = 10,
                    StudentId = students.Single(s => s.LastName == "Li").StudentId,
                    CourseId = courses.Single(c => c.Title == "Composition").CourseId,
                    Grade = Grade.B
                    },
                new EnrollmentEntity {
                    EnrollmentId = 11,
                    StudentId = students.Single(s => s.LastName == "Justice").StudentId,
                    CourseId = courses.Single(c => c.Title == "Literature").CourseId,
                    Grade = Grade.B
                    }
            };

            builder.Entity<StudentEntity>().HasData(students);

            builder.Entity<CourseEntity>().HasData(courses);

            builder.Entity<EnrollmentEntity>().HasData(enrollments);

            builder.Entity<DepartmentEntity>().HasData(departments);

            builder.Entity<InstructorEntity>().HasData(instructors);

            builder.Entity<CourseAssignmentEntity>().HasData(courseInstructors);

            builder.Entity<OfficeAssignmentEntity>().HasData(officeAssignments);
        }
    }
}
