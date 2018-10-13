using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;

namespace ContosoUniversity.Api.Services
{
    public class StudentsesService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;

        private readonly ICoursesRepository _coursesRepository;

        private readonly IMapper _mapper;

        public StudentsesService(IStudentsRepository studentsRepository, ICoursesRepository coursesRepository, IMapper mapper)
        {
            _studentsRepository = studentsRepository;

            _coursesRepository = coursesRepository;

            _mapper = mapper;
        }

        public IEnumerable<Student> GetStudents()
        {
            var studentEntities = _studentsRepository.GetAll();

            return _mapper.Map<IEnumerable<Student>>(studentEntities);
        }

        public Student GetStudentById(int id)
        {
            var studentEntity = _studentsRepository.Get(id);

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

            ValidateEnrollments(student);

            var studentEntity = _mapper.Map<StudentEntity>(student);

            _studentsRepository.Add(studentEntity);

            _studentsRepository.Save("Student");

            var newStudentId = studentEntity.StudentId;

            return GetStudentById(studentEntity.StudentId);
        }

        public Student UpdateStudent(Student student)
        {
            var studentEntity = _mapper.Map<StudentEntity>(student);

            _studentsRepository.Update(studentEntity);

            _studentsRepository.Save("Student");

            return GetStudentById(studentEntity.StudentId);
        }

        public bool RemoveStudent(int studentId)
        {
            var entityStudent = _studentsRepository.Get(studentId);

            _studentsRepository.Remove(entityStudent);

            _studentsRepository.Save("Student");

            return true;
        }

        private void ValidateEnrollments(Student student)
        {
            if (student.Enrollments == null || !student.Enrollments.Any()) return;

            var isNewStudent = student.Enrollments.All(e => e.StudentId == 0);

            if (!isNewStudent)
            {
                throw new InvalidStudentException(
                    "Student in the enrollment must be the same as the new student");
            }

            var courses = _coursesRepository.GetAll();

            foreach (var enrollment in student.Enrollments)
            {
                if (courses.Any(c => c.CourseId != enrollment.CourseId))
                {
                    throw new InvalidCourseException(
                        "One or more chosen course(s) doesnot exist in the database");
                }
            }

            student.Enrollments = null;
        }
    }
}

