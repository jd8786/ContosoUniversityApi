using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using System.Collections.Generic;

namespace ContosoUniversity.Api.AutoMappers
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentEntity, Student>()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => GetCourses(src)))
                .ReverseMap()
                .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => GetEnrollments(src)));
        }

        private static IEnumerable<Course> GetCourses(StudentEntity student)
        {
            var courses = new List<Course>();

            foreach (var enrollment in student.Enrollments)
            {
                var course = new Course
                {
                    CourseId = enrollment.CourseId,
                    Grade = enrollment.Grade,
                    Credits = enrollment.Course.Credits,
                    Title = enrollment.Course.Title
                };

                if (!courses.Contains(course))
                {
                    courses.Add(course);
                }
            }

            return courses;
        }

        private static IEnumerable<EnrollmentEntity> GetEnrollments(Student student)
        {
            var enrollments = new List<EnrollmentEntity>();

            foreach (var course in student.Courses)
            {
                var enrollment = new EnrollmentEntity
                {
                    CourseId = course.CourseId,
                    Grade = course.Grade,
                    StudentId = student.StudentId
                };

                if (!enrollments.Contains(enrollment))
                {
                    enrollments.Add(enrollment);
                }
            }

            return enrollments;
        }
    }
}
