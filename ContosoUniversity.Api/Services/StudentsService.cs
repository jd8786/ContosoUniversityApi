using System.Collections.Generic;
using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Repositories;

namespace ContosoUniversity.Api.Services
{
    public class StudentsService: IStudentService
    {
        private readonly IStudentsRepository _repository;

        private readonly IMapper _mapper;

        public StudentsService(IStudentsRepository repository, IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }
        public List<StudentInfo> GetStudents()
        {
            var students = _repository.GetStudents();

            var studentInfos = _mapper.Map<List<StudentInfo>>(students);

            return studentInfos;
        }
    }
}
