using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace ContosoUniversity.Data.Test.Fixtures
{
    public class InMemoryDbTestFixture: IDisposable
    {
        private readonly DbContextOptions<SchoolContext> _options;

        public SchoolContext Context => new SchoolContext(_options);

        public InMemoryDbTestFixture()
        {
            _options = new DbContextOptionsBuilder<SchoolContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }

        public void InitData()
        {
            using (var context = Context)
            {
                // Instructors
                var instructor1 = new InstructorEntity
                {
                    InstructorId = 1,
                    LastName = "last-name1",
                    FirstMidName = "first-mid-name1",
                    HireDate = new DateTime(2000, 1, 1),
                };

                var instructor2 = new InstructorEntity
                {
                    InstructorId = 2,
                    LastName = "last-name2",
                    FirstMidName = "first-mid-name2",
                    HireDate = new DateTime(2001, 1, 1),
                };

                var instructors = new List<InstructorEntity> { instructor1, instructor2 };

                // OfficeAssignments
                var officeAssignment1 = new OfficeAssignmentEntity
                {
                    InstructorId = 1,
                    Location = "location1"
                };

                var officeAssignment2 = new OfficeAssignmentEntity
                {
                    InstructorId = 2,
                    Location = "location2"
                };

                var officeAssignments = new List<OfficeAssignmentEntity> { officeAssignment1, officeAssignment2 };

                // Departments
                var department1 = new DepartmentEntity
                {
                    DepartmentId = 1,
                    InstructorId = 1,
                    Budget = 1000,
                    Name = "test-name1",
                    StartDate = new DateTime(2000, 1, 1)
                };

                var department2 = new DepartmentEntity
                {
                    DepartmentId = 2,
                    InstructorId = 2,
                    Budget = 2000,
                    Name = "test-name2",
                    StartDate = new DateTime(2001, 1, 1)
                };

                var departments = new List<DepartmentEntity> { department1, department2 };

                // Courses
                var course1 = new CourseEntity
                {
                    CourseId = 1,
                    Credits = 3,
                    Title = "test-title1",
                    DepartmentId = 1,
                };

                var course2 = new CourseEntity
                {
                    CourseId = 2,
                    Credits = 3,
                    Title = "test-title2",
                    DepartmentId = 2,
                };

                var courses = new List<CourseEntity> { course1, course2 };

                // CourseAssignments
                var courseAssignment1 = new CourseAssignmentEntity
                {
                    CourseId = 1,
                    InstructorId = 1
                };

                var courseAssignment2 = new CourseAssignmentEntity
                {
                    CourseId = 2,
                    InstructorId = 2
                };

                var courseAssignments = new List<CourseAssignmentEntity> { courseAssignment1, courseAssignment2 };

                // Students
                var student1 = new StudentEntity
                {
                    StudentId = 1,
                    LastName = "test-last-name1",
                    FirstMidName = "test-last-name1",
                    OriginCountry = "test-origin-country1",
                    EnrollmentDate = new DateTime(2000, 1, 1),
                };

                var student2 = new StudentEntity
                {
                    StudentId = 2,
                    LastName = "test-last-name2",
                    FirstMidName = "test-last-name2",
                    OriginCountry = "test-origin-country2",
                    EnrollmentDate = new DateTime(2001, 1, 1),
                };

                var students = new List<StudentEntity> { student1, student2 };

                // Enrollments
                var enrollment1 = new EnrollmentEntity
                {
                    CourseId = 1,
                    StudentId = 1,
                    Grade = Grade.A
                };

                var enrollment2 = new EnrollmentEntity
                {
                    CourseId = 2,
                    StudentId = 2
                };

                var enrollments = new List<EnrollmentEntity> { enrollment1, enrollment2 };

                // Add and Save to Db
                context.Instructors.AddRange(instructors);
                context.OfficeAssignments.AddRange(officeAssignments);
                context.Departments.AddRange(departments);
                context.Courses.AddRange(courses);
                context.CourseAssignments.AddRange(courseAssignments);
                context.Students.AddRange(students);
                context.Enrollments.AddRange(enrollments);

                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            Context?.Database.EnsureDeleted();
            Context?.Dispose();
        }
    }
}
