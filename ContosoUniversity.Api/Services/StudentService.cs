using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Api.Services
{
    public class StudentService : MapperService<StudentEntity, Student>, IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        private readonly IStudentValidator _studentValidator;

        public StudentService(IStudentRepository studentRepository, IStudentValidator studentValidator, IMapper mapper): base(mapper)
        {
            _studentRepository = studentRepository;

            _studentValidator = studentValidator;
        }

        public IEnumerable<Student> GetAll()
        {
            var studentEntities = _studentRepository.GetAll();

            return MapToModels(studentEntities);
        }

        public Student Get(int id)
        {
            _studentValidator.CommonValidator.ValidateStudentById(id);

            var studentEntity = _studentRepository.GetAll().First(s => s.StudentId == id);

            return MapToModel(studentEntity);
        }

        public Student Add(Student student)
        {
            _studentValidator.ValidatePostStudent(student);

            var studentEntity = MapToEntity(student);

            _studentRepository.Add(studentEntity);

            _studentRepository.Save();

            return Get(studentEntity.StudentId);
        }

        public Student Update(Student student)
        {
            _studentValidator.ValidatePutStudent(student);

            var studentEntity = MapToEntity(student);

            _studentRepository.Update(studentEntity);

            _studentRepository.Save();

            return Get(studentEntity.StudentId);
        }

        public bool Remove(int studentId)
        {
            _studentValidator.CommonValidator.ValidateStudentById(studentId);

            var entityStudent = _studentRepository.GetAll().First(s => s.StudentId == studentId);

            _studentRepository.Remove(entityStudent);

            _studentRepository.Save();

            return true;
        }
    }
}

