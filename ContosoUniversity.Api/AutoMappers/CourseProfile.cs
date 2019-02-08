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
                .ReverseMap()
                .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => GetEnrollments(src)));
        }

        private static IEnumerable<Student> GetStudents(CourseEntity course)
        {
            var students = new List<Student>();

            foreach (var enrollment in course.Enrollments)
            {
                var student = new Student
                {
                    StudentId = enrollment.CourseId,
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
