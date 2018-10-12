using System.Collections.Generic;
using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;

namespace ContosoUniversity.Api.Services
{
    public class StudentsService : IStudentService
    {
        private readonly IStudentsRepository _repository;

        private readonly IMapper _mapper;

        public StudentsService(IStudentsRepository repository, IMapper mapper)
        {
            _repository = repository;

            _mapper = mapper;
        }

        public IEnumerable<Student> GetStudents()
        {
            var studentEntities = _repository.GetAll();

            return _mapper.Map<IEnumerable<Student>>(studentEntities);
        }

        public Student GetStudentById(int id)
        {
            var studentEntity = _repository.Get(id);

            if (studentEntity == null)
            {
                throw new NotFoundException("Student was not found");
            }

            return _mapper.Map<Student>(studentEntity);
        }

        public Student AddStudent(Student student)
        {
            if (student == null)
            {
                throw new InvalidStudentException("Student must be provided");
            }

            if (student.StudentId != 0)
            {
                throw new InvalidStudentException("Student Id has to be 0");
            }

            var studentEntity = _mapper.Map<StudentEntity>(student);

            _repository.Add(studentEntity);

            _repository.Save("Student");

            return GetStudentById(studentEntity.StudentId);
        }

        public Student UpdateStudent(Student student)
        {
            var studentEntity = _mapper.Map<StudentEntity>(student);

            _repository.Update(studentEntity);

            _repository.Save("Student");

            return GetStudentById(studentEntity.StudentId);
        }

        public bool RemoveStudent(int studentId)
        {
            var entityStudent = _repository.Get(studentId);

            _repository.Remove(entityStudent);

            _repository.Save("Student");

            return true;
        }
    }
}

