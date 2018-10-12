using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Api.AutoMappers
{
    public class StudentEntityToStudentProfile : Profile
    {
        public StudentEntityToStudentProfile()
        {
            CreateMap<StudentEntity, Student>();
        }
    }
}
