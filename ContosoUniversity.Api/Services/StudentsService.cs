using System.Collections.Generic;
using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Models;
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
        public List<StudentInfo> GetStudentInfos()
        {
            var students = _repository.GetStudents();

            var studentInfos = _mapper.Map<List<StudentInfo>>(students);

            return studentInfos;
        }

        public StudentInfo GetStudentInfoById(int studentInfoId)
        {
            var student = _repository.GetStudentById(studentInfoId);

            var studentInfo = _mapper.Map<StudentInfo>(student);

            return studentInfo;
        }

        public void CreateStudentInfo(StudentInfo studentInfo)
        {
            var student = _mapper.Map<Student>(studentInfo);

            _repository.CreateStudent(student);
        }

        public bool UpdateStudentInfo(StudentInfo studentInfo)
        {
            var student = _mapper.Map<Student>(studentInfo);

            return _repository.UpdateStudent(student);
        }

        public bool DeleteStudentInfo(int studentInfoId)
        {
            return _repository.DeleteStudent(studentInfoId);
        }
    }
}

