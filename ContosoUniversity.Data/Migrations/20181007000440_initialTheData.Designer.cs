﻿// <auto-generated />
using System;
using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContosoUniversity.Data.Migrations
{
    [DbContext(typeof(SchoolContext))]
    [Migration("20181007000440_initialTheData")]
    partial class initialTheData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.CourseAssignmentEntity", b =>
                {
                    b.Property<int>("CourseAssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId");

                    b.Property<int>("InstructorId");

                    b.HasKey("CourseAssignmentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId");

                    b.ToTable("CourseAssignment");

                    b.HasData(
                        new { CourseAssignmentId = 1, CourseId = 1050, InstructorId = 4 },
                        new { CourseAssignmentId = 2, CourseId = 1050, InstructorId = 3 },
                        new { CourseAssignmentId = 3, CourseId = 4022, InstructorId = 5 },
                        new { CourseAssignmentId = 4, CourseId = 4041, InstructorId = 5 },
                        new { CourseAssignmentId = 5, CourseId = 1045, InstructorId = 2 },
                        new { CourseAssignmentId = 6, CourseId = 3141, InstructorId = 3 },
                        new { CourseAssignmentId = 7, CourseId = 2021, InstructorId = 1 },
                        new { CourseAssignmentId = 8, CourseId = 2042, InstructorId = 1 }
                    );
                });

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.CourseEntity", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Credits");

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("CourseId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Course");

                    b.HasData(
                        new { CourseId = 1050, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 247, DateTimeKind.Local), Credits = 3, DepartmentId = 3, Title = "Chemistry" },
                        new { CourseId = 4022, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), Credits = 3, DepartmentId = 4, Title = "Microeconomics" },
                        new { CourseId = 4041, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), Credits = 3, DepartmentId = 4, Title = "Macroeconomics" },
                        new { CourseId = 1045, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), Credits = 4, DepartmentId = 2, Title = "Calculus" },
                        new { CourseId = 3141, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), Credits = 4, DepartmentId = 2, Title = "Trigonometry" },
                        new { CourseId = 2021, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), Credits = 3, DepartmentId = 1, Title = "Composition" },
                        new { CourseId = 2042, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 248, DateTimeKind.Local), Credits = 4, DepartmentId = 1, Title = "Literature" }
                    );
                });

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.DepartmentEntity", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Budget");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("InstructorId");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("DepartmentId");

                    b.HasIndex("InstructorId");

                    b.ToTable("Department");

                    b.HasData(
                        new { DepartmentId = 1, Budget = 350000m, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 245, DateTimeKind.Local), InstructorId = 1, Name = "English", StartDate = new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { DepartmentId = 2, Budget = 100000m, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 246, DateTimeKind.Local), InstructorId = 2, Name = "Mathematics", StartDate = new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { DepartmentId = 3, Budget = 350000m, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 246, DateTimeKind.Local), InstructorId = 3, Name = "Engineering", StartDate = new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { DepartmentId = 4, Budget = 100000m, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 246, DateTimeKind.Local), InstructorId = 4, Name = "Economics", StartDate = new DateTime(2007, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                    );
                });

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.EnrollmentEntity", b =>
                {
                    b.Property<int>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId");

                    b.Property<int?>("Grade");

                    b.Property<int>("StudentId");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollment");

                    b.HasData(
                        new { EnrollmentId = 1, CourseId = 1050, Grade = 0, StudentId = 1 },
                        new { EnrollmentId = 2, CourseId = 4022, Grade = 2, StudentId = 1 },
                        new { EnrollmentId = 3, CourseId = 4041, Grade = 1, StudentId = 1 },
                        new { EnrollmentId = 4, CourseId = 1045, Grade = 1, StudentId = 2 },
                        new { EnrollmentId = 5, CourseId = 3141, Grade = 1, StudentId = 2 },
                        new { EnrollmentId = 6, CourseId = 2021, Grade = 1, StudentId = 2 },
                        new { EnrollmentId = 7, CourseId = 1050, StudentId = 3 },
                        new { EnrollmentId = 8, CourseId = 4022, Grade = 1, StudentId = 3 },
                        new { EnrollmentId = 9, CourseId = 1050, Grade = 1, StudentId = 4 },
                        new { EnrollmentId = 10, CourseId = 2021, Grade = 1, StudentId = 5 },
                        new { EnrollmentId = 11, CourseId = 2042, Grade = 1, StudentId = 6 }
                    );
                });

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.InstructorEntity", b =>
                {
                    b.Property<int>("InstructorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("HireDate");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("InstructorId");

                    b.ToTable("Instructor");

                    b.HasData(
                        new { InstructorId = 1, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local), FirstMidName = "Kim", HireDate = new DateTime(1995, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), LastName = "Abercrombie" },
                        new { InstructorId = 2, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local), FirstMidName = "Fadi", HireDate = new DateTime(2002, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), LastName = "Fakhouri" },
                        new { InstructorId = 3, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local), FirstMidName = "Roger", HireDate = new DateTime(1998, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), LastName = "Harui" },
                        new { InstructorId = 4, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local), FirstMidName = "Candace", HireDate = new DateTime(2001, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), LastName = "Kapoor" },
                        new { InstructorId = 5, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 244, DateTimeKind.Local), FirstMidName = "Roger", HireDate = new DateTime(2004, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), LastName = "Zheng" }
                    );
                });

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.OfficeAssignmentEntity", b =>
                {
                    b.Property<int>("InstructorId");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("InstructorId");

                    b.ToTable("OfficeAssignment");

                    b.HasData(
                        new { InstructorId = 2, Location = "Smith 17" },
                        new { InstructorId = 3, Location = "Gowan 27" },
                        new { InstructorId = 4, Location = "Thompson 304" }
                    );
                });

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.StudentEntity", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("EnrollmentDate");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("OriginCountry")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("StudentId");

                    b.ToTable("Student");

                    b.HasData(
                        new { StudentId = 1, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 236, DateTimeKind.Local), EnrollmentDate = new DateTime(2010, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstMidName = "Carson", LastName = "Alexander", OriginCountry = "USA" },
                        new { StudentId = 2, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), EnrollmentDate = new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstMidName = "Meredith", LastName = "Alonso", OriginCountry = "UK" },
                        new { StudentId = 3, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), EnrollmentDate = new DateTime(2013, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstMidName = "Arturo", LastName = "Anand", OriginCountry = "TURKEY" },
                        new { StudentId = 4, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), EnrollmentDate = new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstMidName = "Gytis", LastName = "Barzdukas", OriginCountry = "CHINA" },
                        new { StudentId = 5, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), EnrollmentDate = new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstMidName = "Yan", LastName = "Li", OriginCountry = "JAPAN" },
                        new { StudentId = 6, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), EnrollmentDate = new DateTime(2011, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstMidName = "Peggy", LastName = "Justice", OriginCountry = "UK" },
                        new { StudentId = 7, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), EnrollmentDate = new DateTime(2013, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstMidName = "Laura", LastName = "Norman", OriginCountry = "USA" },
                        new { StudentId = 8, CreatedBy = "ContosoUniversityUsers", CreatedDate = new DateTime(2018, 10, 6, 20, 4, 39, 243, DateTimeKind.Local), EnrollmentDate = new DateTime(2005, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), FirstMidName = "Nino", LastName = "Olivetto", OriginCountry = "RUSSIA" }
                    );
                });

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.CourseAssignmentEntity", b =>
                {
                    b.HasOne("ContosoUniversity.Data.EntityModels.CourseEntity", "Course")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContosoUniversity.Data.EntityModels.InstructorEntity", "Instructor")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.CourseEntity", b =>
                {
                    b.HasOne("ContosoUniversity.Data.EntityModels.DepartmentEntity", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.DepartmentEntity", b =>
                {
                    b.HasOne("ContosoUniversity.Data.EntityModels.InstructorEntity", "Administrator")
                        .WithMany()
                        .HasForeignKey("InstructorId");
                });

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.EnrollmentEntity", b =>
                {
                    b.HasOne("ContosoUniversity.Data.EntityModels.CourseEntity", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ContosoUniversity.Data.EntityModels.StudentEntity", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContosoUniversity.Data.EntityModels.OfficeAssignmentEntity", b =>
                {
                    b.HasOne("ContosoUniversity.Data.EntityModels.InstructorEntity", "Instructor")
                        .WithOne("OfficeAssignment")
                        .HasForeignKey("ContosoUniversity.Data.EntityModels.OfficeAssignmentEntity", "InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
