using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentsRepository;

        private readonly IStudentValidator _studentValidator;

        private readonly ICourseValidator _courseValidator;

        private readonly IMapper _mapper;

        public StudentService(IStudentRepository studentsRepository, IStudentValidator studentValidator, ICourseValidator courseValidator,  IMapper mapper)
        {
            _studentsRepository = studentsRepository;

            _studentValidator = studentValidator;

            _courseValidator = courseValidator;

            _mapper = mapper;
        }

        public IEnumerable<Student> GetAll()
        {
            var studentEntities = _studentsRepository.GetAll();

            return _mapper.Map<IEnumerable<Student>>(studentEntities);
        }

        public Student Get(int id)
        {
            _studentValidator.Validate(id);

            var studentEntity = _studentsRepository.GetAll().FirstOrDefault(s => s.StudentId == id);

            return _mapper.Map<Student>(studentEntity);
        }

        public Student Add(Student student)
        {
            _studentValidator.ValidatePostStudent(student);

            if (student.Courses != null && student.Courses.Any())
            {
                foreach (var course in student.Courses)
                {
                    _courseValidator.Validate(course.CourseId);
                }
            }

            var studentEntity = _mapper.Map<StudentEntity>(student);

            _studentsRepository.Add(studentEntity);

            _studentsRepository.Save();

            return Get(studentEntity.StudentId);
        }

        public Student Update(Student student)
        {
            _studentValidator.ValidatePutStudent(student);

            if (student.Courses != null && student.Courses.Any())
            {
                foreach (var course in student.Courses)
                {
                    _courseValidator.Validate(course.CourseId);
                }
            }

            var studentEntity = _mapper.Map<StudentEntity>(student);

            _studentsRepository.Update(studentEntity);

            _studentsRepository.Save();

            return Get(studentEntity.StudentId);
        }

        public bool Remove(int studentId)
        {
            var student = Get(studentId);

            var entityStudent = _mapper.Map<StudentEntity>(student);

            _studentsRepository.Remove(entityStudent);

            _studentsRepository.Save();

            return true;
        }
    }
}

