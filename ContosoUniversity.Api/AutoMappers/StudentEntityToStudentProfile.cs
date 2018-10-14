using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Api.AutoMappers
{
    public class StudentEntityToStudentProfile : Profile
    {
        public StudentEntityToStudentProfile()
        {
            CreateMap<StudentEntity, Student>()
                .ReverseMap()
                .ForMember(dest => dest.Enrollments, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore());
        }
    }
}
