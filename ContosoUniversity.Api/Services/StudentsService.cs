using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Validators;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Api.Services
{
    public class StudentsesService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;

        private readonly IEnrollmentsRepository _enrollmentsRepository;

        private readonly IStudentValidator _studentValidator;

        private readonly IMapper _mapper;

        public StudentsesService(IStudentsRepository studentsRepository, IEnrollmentsRepository enrollmentsRepository, IStudentValidator studentValidator, IMapper mapper)
        {
            _studentsRepository = studentsRepository;

            _enrollmentsRepository = enrollmentsRepository;

            _studentValidator = studentValidator;

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

            var studentEntity = _mapper.Map<StudentEntity>(student);

            studentEntity.Enrollments = null;

            _studentsRepository.Add(studentEntity);

            _studentsRepository.Save("Student");

            return Get(studentEntity.StudentId);
        }

        public Student Update(Student student)
        {
            _studentValidator.ValidatePutStudent(student);

            var existingStudent = Get(student.StudentId);

            existingStudent.Enrollments = _mapper.Map<IEnumerable<Enrollment>>(_enrollmentsRepository.GetByStudentId(student.StudentId));

            existingStudent.EnrollmentDate = student.EnrollmentDate;

            existingStudent.FirstMidName = student.FirstMidName;

            existingStudent.LastName = student.LastName;

            existingStudent.OriginCountry = student.OriginCountry;

            var studentEntity = _mapper.Map<StudentEntity>(existingStudent);

            _studentsRepository.Update(studentEntity);

            _studentsRepository.Save("Student");

            return Get(studentEntity.StudentId);
        }

        public bool Remove(int studentId)
        {
            var student = Get(studentId);

            var entityStudent = _mapper.Map<StudentEntity>(student);

            _studentsRepository.Remove(entityStudent);

            _studentsRepository.Save("Student");

            return true;
        }
    }
}

