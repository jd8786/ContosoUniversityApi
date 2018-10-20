using System.Collections.Generic;
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

        private readonly IEnrollmentsService _enrollmentsService;

        private readonly IMapper _mapper;

        public StudentsesService(IStudentsRepository studentsRepository, IEnrollmentsService enrollmentsService, IMapper mapper)
        {
            _studentsRepository = studentsRepository;

            _enrollmentsService = enrollmentsService;

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
                throw new NotFoundException($"Student with id {id} was not found");
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
                throw new InvalidStudentException("Student Id must be 0");
            }

            var studentEntity = _mapper.Map<StudentEntity>(student);

            studentEntity.Enrollments = null;

            _studentsRepository.Add(studentEntity);

            _studentsRepository.Save("Student");

            if (student.Enrollments != null)
            {
               _enrollmentsService.AddRange(student.Enrollments);
            }

            return Get(studentEntity.StudentId);
        }

        public Student Update(Student student)
        {
            if (student == null)
            {
                throw new InvalidStudentException("Student must be provided");
            }

            if (student.StudentId == 0)
            {
                throw new InvalidStudentException("Student Id cannot be 0");
            }

            var existingStudent = Get(student.StudentId);

            existingStudent.Enrollments = student.Enrollments;

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
            var entityStudent = _studentsRepository.Get(studentId);

            _studentsRepository.Remove(entityStudent);

            _studentsRepository.Save("Student");

            return true;
        }
    }
}

