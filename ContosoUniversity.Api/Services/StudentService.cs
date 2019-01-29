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
        private readonly IStudentRepository _studentsRepository;

        private readonly IStudentValidator _studentValidator;

        private readonly ICourseValidator _courseValidator;

        public StudentService(IStudentRepository studentsRepository, IStudentValidator studentValidator, ICourseValidator courseValidator,  IMapper mapper): base(mapper)
        {
            _studentsRepository = studentsRepository;

            _studentValidator = studentValidator;

            _courseValidator = courseValidator;
        }

        public IEnumerable<Student> GetAll()
        {
            var studentEntities = _studentsRepository.GetAll();

            return MapToModels(studentEntities);
        }

        public Student Get(int id)
        {
            _studentValidator.ValidateById(id);

            var studentEntity = _studentsRepository.GetAll().FirstOrDefault(s => s.StudentId == id);

            return MapToModel(studentEntity);
        }

        public Student Add(Student student)
        {
            _studentValidator.ValidatePostStudent(student);

            if (student.Courses != null && student.Courses.Any())
            {
                foreach (var course in student.Courses)
                {
                    _courseValidator.ValidateById(course.CourseId);
                }
            }

            var studentEntity = MapToEntity(student);

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
                    _courseValidator.ValidateById(course.CourseId);
                }
            }

            var studentEntity = MapToEntity(student);

            _studentsRepository.Update(studentEntity);

            _studentsRepository.Save();

            return Get(studentEntity.StudentId);
        }

        public bool Remove(int studentId)
        {
            var student = Get(studentId);

            var entityStudent = MapToEntity(student);

            _studentsRepository.Remove(entityStudent);

            _studentsRepository.Save();

            return true;
        }
    }
}

