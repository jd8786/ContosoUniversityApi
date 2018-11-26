using System;
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

        private readonly IEnrollmentsService _enrollmentsService;

        private readonly ICoursesRepository _coursesRepository;

        private readonly IMapper _mapper;

        public StudentsesService(IStudentsRepository studentsRepository, IEnrollmentsService enrollmentsService, ICoursesRepository coursesRepository, IMapper mapper)
        {
            _studentsRepository = studentsRepository;

            _coursesRepository = coursesRepository;

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

            var courseIds = studentEntity.Enrollments.Select(e => e.CourseId).ToList();

            if (courseIds.Any())
            {
                foreach (var courseId in courseIds)
                {
                    var courseEntity = _coursesRepository.Get(courseId);

                    if (courseEntity == null)
                    {
                        throw new NotFoundException($"Course provided with Id {courseId} doesnot exist in the database");
                    }
                }
            }

            _studentsRepository.Add(studentEntity);

            _studentsRepository.Save("Student");

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

            student.Enrollments = student.Enrollments ?? new List<Enrollment>();

            if (!Equals(existingStudent.Enrollments, student.Enrollments))
            {
                var existingEnrollments = _enrollmentsService.FindByStudentId(student.StudentId);

                _enrollmentsService.RemoveRange(existingEnrollments);

                _enrollmentsService.AddRange(student.Enrollments);
            }

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

