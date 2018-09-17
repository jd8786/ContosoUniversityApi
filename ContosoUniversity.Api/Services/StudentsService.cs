using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<List<StudentInfo>> GetStudentInfosAsync()
        {
            var students = await _repository.GetStudentsAsync();

            return _mapper.Map<List<StudentInfo>>(students);
        }

        public async Task<StudentInfo> GetStudentInfoByIdAsync(int studentInfoId)
        {
            var student = await _repository.GetStudentByIdAsync(studentInfoId);

            return _mapper.Map<StudentInfo>(student);
        }

        public async Task<StudentInfo> CreateStudentInfoAsync(StudentInfo studentInfo)
        {
            var student = _mapper.Map<Student>(studentInfo);

            var newStudent = await _repository.CreateAsync(student);

            return _mapper.Map<StudentInfo>(newStudent);
        }

        public async Task<bool> UpdateStudentInfoAsync(StudentInfo studentInfo)
        {
            var student = _mapper.Map<Student>(studentInfo);

            return await _repository.UpdateAsync(student);
        }

        public async Task<bool> DeleteStudentInfoAsync(int studentInfoId)
        {
            return await _repository.DeleteAsync(studentInfoId);
        }
    }
}

