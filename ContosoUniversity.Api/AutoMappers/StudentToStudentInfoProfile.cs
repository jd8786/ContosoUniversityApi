using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Models;
using Microsoft.CodeAnalysis;

namespace ContosoUniversity.Api.AutoMappers
{
    public class StudentToStudentInfoProfile: Profile
    {
        public StudentToStudentInfoProfile()
        {
            CreateMap<Student, StudentInfo>()
                .ForMember(m => m.StudentInfoId, opt => opt.MapFrom(src => src.StudentId));
        }
    }
}
