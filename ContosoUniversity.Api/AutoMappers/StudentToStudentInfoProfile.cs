using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Models;

namespace ContosoUniversity.Api.AutoMappers
{
    public class StudentToStudentInfoProfile : Profile
    {
        public StudentToStudentInfoProfile()
        {
            CreateMap<Student, StudentInfo>()
                .ForMember(m => m.StudentInfoId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(m => m.CourseInfos, opt => opt.MapFrom(src => MapCourseInfo(src)));
        }

        private static List<CourseInfo> MapCourseInfo(Student student)
        {
            var courseInfos = new List<CourseInfo>();

            var courses = student?.Enrollments?.Select(e => new { e.Course.CourseId, e.Course.Credits, e.Course.Title, e.Grade });

            if (courses == null) return courseInfos;

            courseInfos.AddRange(courses.Select(course => new CourseInfo
            {
                CourseInfoId = course.CourseId,
                Credits = course.Credits,
                Grade = course.Grade,
                Title = course.Title
            }));

            return courseInfos;
        }
    }
}
