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

        public IEnumerable<Student> GetAll()
        {
            var studentEntities = _studentsRepository.GetAll();

            return _mapper.Map<IEnumerable<Student>>(studentEntities);
        }

        public Student Get(int id)
        {
            var studentEntity = _studentsRepository.Get(id);

            if (studentEntity == null)
            {
                throw new NotFoundException("Student was not found");
            }

            return _mapper.Map<Student>(studentEntity);
        }

        public Student Add(Student student)
        {
            if (student == null)
            {
                throw new InvalidStudentException("Student must be provided");
            }

            if (student.StudentId != 0)
            {
                throw new InvalidStudentException("Student Id has to be 0");
            }

            if (student.Enrollments != null && student.Enrollments.Any())
            {
                throw new InvalidStudentException("Enrollment cannot be done before student is added");
            }
            //ValidateEnrollments(student);

            var studentEntity = _mapper.Map<StudentEntity>(student);

            _studentsRepository.Add(studentEntity);

            _studentsRepository.Save("Student");

            return Get(studentEntity.StudentId);
        }

        public Student Update(Student student)
        {
            var studentEntity = _mapper.Map<StudentEntity>(student);

            _studentsRepository.Update(studentEntity);

            _studentsRepository.Save("Student");

            return Get(studentEntity.StudentId);
        }

        public bool Remove(int studentId)
        {
            var entityStudent = _studentsRepository.Get(studentId);

            _studentsRepository.Remove(entityStudent);

            _studentsRepository.Save("Student");

            return true;
        }

        //private void ValidateEnrollments(Student student)
        //{
        //    if (student.Enrollments == null || !student.Enrollments.Any()) return;

        //    var isNewStudent = student.Enrollments.All(e => e.StudentId == 0);

        //    if (!isNewStudent)
        //    {
        //        throw new InvalidStudentException(
        //            "Student in the enrollment must be the new student");
        //    }

        //    var courses = _coursesRepository.GetAll();

        //    foreach (var enrollment in student.Enrollments)
        //    {
        //        var isCourseExisting = courses.FirstOrDefault(c => c.CourseId == enrollment.CourseId) != null;

        //        if (!isCourseExisting)
        //        {
        //            throw new InvalidCourseException(
        //                "One or more chosen course(s) doesnot exist in the database");
        //        }
        //    }
        //}
    }
}

