using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using System.Collections.Generic;

namespace ContosoUniversity.Api.AutoMappers
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseEntity, Course>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => GetStudents(src)))
                .ForMember(dest => dest.Instructors, opt => opt.MapFrom(src => GetInstructors(src)))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
                .ReverseMap()
                .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => GetEnrollments(src)))
                .ForMember(dest => dest.CourseAssignments, opt => opt.MapFrom(src => GetCourseAssignments(src)))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department.DepartmentId));
        }

        private static IEnumerable<Student> GetStudents(CourseEntity course)
        {
            var students = new List<Student>();

            foreach (var enrollment in course.Enrollments)
            {
                var student = new Student
                {
                    StudentId = enrollment.StudentId,
                    EnrollmentDate = enrollment.Student.EnrollmentDate,
                    FirstMidName = enrollment.Student.FirstMidName,
                    LastName = enrollment.Student.LastName,
                    OriginCountry = enrollment.Student.OriginCountry
                };

                if (!students.Contains(student))
                {
                    students.Add(student);
                }
            }

            return students;
        }

        private static IEnumerable<EnrollmentEntity> GetEnrollments(Course course)
        {
            var enrollments = new List<EnrollmentEntity>();

            foreach (var student in course.Students)
            {
                var enrollment = new EnrollmentEntity
                {
                    CourseId = course.CourseId,
                    StudentId = student.StudentId
                };

                if (!enrollments.Contains(enrollment))
                {
                    enrollments.Add(enrollment);
                }
            }

            return enrollments;
        }

        private static IEnumerable<Instructor> GetInstructors(CourseEntity course)
        {
            var instructors = new List<Instructor>();

            foreach (var courseAssignment in course.CourseAssignments)
            {
                var instructor = new Instructor
                {
                    InstructorId = courseAssignment.InstructorId,
                    FirstMidName = courseAssignment.Instructor.FirstMidName,
                    HireDate = courseAssignment.Instructor.HireDate,
                    LastName = courseAssignment.Instructor.LastName,
                    OfficeLocation = courseAssignment.Instructor.OfficeAssignment.Location
                };

                if (!instructors.Contains(instructor))
                {
                    instructors.Add(instructor);
                }
            }

            return instructors;
        }

        private static IEnumerable<CourseAssignmentEntity> GetCourseAssignments(Course course)
        {
            var courseAssignments = new List<CourseAssignmentEntity>();

            foreach (var instructor in course.Instructors)
            {
                var courseAssignment = new CourseAssignmentEntity
                {
                    CourseId = course.CourseId,
                    InstructorId = instructor.InstructorId
                };

                if (!courseAssignments.Contains(courseAssignment))
                {
                    courseAssignments.Add(courseAssignment);
                }
            }

            return courseAssignments;
        }
    }
}
