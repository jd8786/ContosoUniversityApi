using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Api.AutoMappers
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentEntity, Department>()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses))
                .ForMember(dest => dest.Administrator, opt => opt.MapFrom(src => src.Administrator))
                .ReverseMap()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses))
                .ForMember(dest => dest.InstructorId, opt => opt.MapFrom(src => src.Administrator.InstructorId));
        }
    }
}
