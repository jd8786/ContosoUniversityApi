using ContosoUniversity.Data;
using ContosoUniversity.Data.EntityModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ContosoUniversity.Api.Acceptance.Test.Fixtures
{
    public class AcceptanceTestFixture: IDisposable
    {
        public readonly WebApplicationFactory<Startup> Factory;

        public readonly HttpClient HttpClient;

        public SchoolContext SchoolContext => GetService<SchoolContext>();

        public AcceptanceTestFixture()
        {
            Factory = new WebApplicationFactory<Startup>();

            HttpClient = Factory.CreateDefaultClient();
        }

        private T GetService<T>()
        {
            return Factory.Server.Host.Services.CreateScope().ServiceProvider.GetRequiredService<T>();
        }

        public void ResetDatabase()
        {
            CleanDatabase();
            SeedDatabase();
        }

        private void CleanDatabase()
        {
            using (var context = SchoolContext)
            {
                context.OfficeAssignments.RemoveRange(context.OfficeAssignments);
                context.CourseAssignments.RemoveRange(context.CourseAssignments);
                context.Instructors.RemoveRange(context.Instructors);
                context.Departments.RemoveRange(context.Departments);
                context.Enrollments.RemoveRange(context.Enrollments);
                context.Courses.RemoveRange(context.Courses);
                context.Students.RemoveRange(context.Students);

                context.SaveChanges();
            }
        }

        private void SeedDatabase()
        {
            using (var context = SchoolContext)
            {
                var students = new List<StudentEntity>
                {
                    new StudentEntity
                    {
                        FirstMidName = "test-first-mid-name1",
                        LastName = "test-last-name1",
                        EnrollmentDate = DateTime.Parse("2010-09-01"),
                        OriginCountry = "test-country1"
                    },
                    new StudentEntity
                    {
                        FirstMidName = "test-first-mid-name2",
                        LastName = "test-last-name2",
                        EnrollmentDate = DateTime.Parse("2012-09-01"),
                        OriginCountry = "test-country2"
                    }
                };

                context.Students.AddRange(students);

                var instructors = new List<InstructorEntity>
                {
                    new InstructorEntity
                    {
                        FirstMidName = "test-first-mid-name1",
                        LastName = "test-last-name1",
                        HireDate = DateTime.Parse("1995-03-11")
                    },
                    new InstructorEntity
                    {
                        FirstMidName = "test-first-mid-name2",
                        LastName = "test-last-name2",
                        HireDate = DateTime.Parse("2002-07-06")
                    }
                };

                context.Instructors.AddRange(instructors);

                var departments = new List<DepartmentEntity>
                {
                    new DepartmentEntity
                    {
                        Name = "test-name1",
                        Budget = 350000,
                        StartDate = DateTime.Parse("2007-09-01"),
                        InstructorId = instructors.Single( i => i.LastName == "test-last-name1")?.InstructorId
                    },
                    new DepartmentEntity
                    {
                        Name = "test-name2",
                        Budget = 100000,
                        StartDate = DateTime.Parse("2007-09-01"),
                        InstructorId = instructors.Single( i => i.LastName == "test-last-name2")?.InstructorId
                    },
                };

                context.Departments.AddRange(departments);

                var courses = new List<CourseEntity>
                {
                    new CourseEntity
                    {
                        CourseId = 1050,
                        Title = "test-title1",
                        Credits = 3,
                        DepartmentId = departments.Single( s => s.Name == "test-name1").DepartmentId
                    },
                    new CourseEntity
                    {
                        CourseId = 4022,
                        Title = "test-title2",
                        Credits = 3,
                        DepartmentId = departments.Single( s => s.Name == "test-name2").DepartmentId
                    }
                };

                context.Courses.AddRange(courses);

                var officeAssignments = new List<OfficeAssignmentEntity>
                {
                    new OfficeAssignmentEntity
                    {
                        InstructorId = instructors.Single( i => i.LastName == "test-last-name1").InstructorId,
                        Location = "test-location1"
                    },
                    new OfficeAssignmentEntity
                    {
                        InstructorId = instructors.Single( i => i.LastName == "test-last-name2").InstructorId,
                        Location = "test-location2"
                    }
                };

                context.OfficeAssignments.AddRange(officeAssignments);

                var courseInstructors = new List<CourseAssignmentEntity>
                {
                    new CourseAssignmentEntity
                    {
                        CourseId = courses.Single(c => c.Title == "test-title1" ).CourseId,
                        InstructorId = instructors.Single(i => i.LastName == "test-last-name1").InstructorId
                    },
                    new CourseAssignmentEntity
                    {
                        CourseId = courses.Single(c => c.Title == "test-title2" ).CourseId,
                        InstructorId = instructors.Single(i => i.LastName == "test-last-name2").InstructorId
                    }
                };

                context.CourseAssignments.AddRange(courseInstructors);

                var enrollments = new EnrollmentEntity[]
                {
                    new EnrollmentEntity
                    {
                        StudentId = students.Single(s => s.LastName == "test-last-name1").StudentId,
                        CourseId = courses.Single(c => c.Title == "test-title1" ).CourseId,
                        Grade = Grade.A
                    },
                    new EnrollmentEntity
                    {
                        StudentId = students.Single(s => s.LastName == "test-last-name2").StudentId,
                        CourseId = courses.Single(c => c.Title == "test-title2" ).CourseId,
                        Grade = Grade.C
                    }
                };

                context.Enrollments.AddRange(enrollments);

                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            SchoolContext?.Dispose();
            HttpClient?.Dispose();
            Factory?.Dispose();
        }
    }
}
