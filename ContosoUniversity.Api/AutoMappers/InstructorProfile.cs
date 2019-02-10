using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using System.Collections.Generic;

namespace ContosoUniversity.Api.AutoMappers
{
    public class InstructorProfile : Profile
    {
        public InstructorProfile()
        {
            CreateMap<InstructorEntity, Instructor>()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => GetCourses(src)))
                .ForMember(dest => dest.OfficeLocation, opt => opt.MapFrom(src => src.OfficeAssignment.Location))
                .ReverseMap()
                .ForMember(dest => dest.CourseAssignments, opt => opt.MapFrom(src => GetCourseAssignments(src)))
                .ForMember(dest => dest.OfficeAssignment.InstructorId, opt => opt.MapFrom(src => src.InstructorId))
                .ForMember(dest => dest.OfficeAssignment.Location, opt => opt.MapFrom(src => src.OfficeLocation));
        }

        private static IEnumerable<Course> GetCourses(InstructorEntity instructor)
        {
            var courses = new List<Course>();

            foreach (var courseAssignment in instructor.CourseAssignments)
            {
                var course = new Course
                {
                    CourseId = courseAssignment.CourseId,
                    Credits = courseAssignment.Course.Credits,
                    Title = courseAssignment.Course.Title
                };

                if (!courses.Contains(course))
                {
                    courses.Add(course);
                }
            }

            return courses;
        }

        private static IEnumerable<CourseAssignmentEntity> GetCourseAssignments(Instructor instructor)
        {
            var courseAssignments = new List<CourseAssignmentEntity>();

            foreach (var course in instructor.Courses)
            {
                var courseAssignment = new CourseAssignmentEntity
                {
                    InstructorId = instructor.InstructorId,
                    CourseId = course.CourseId
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
