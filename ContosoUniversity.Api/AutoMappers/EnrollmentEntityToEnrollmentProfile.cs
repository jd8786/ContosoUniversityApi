using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Api.AutoMappers
{
    public class EnrollmentEntityToEnrollmentProfile : Profile
    {
        public EnrollmentEntityToEnrollmentProfile()
        {
            CreateMap<EnrollmentEntity, Enrollment>()
                .ReverseMap();
        }
    }
}
