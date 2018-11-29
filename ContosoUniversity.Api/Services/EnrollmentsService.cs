using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Api.Services
{
    public class EnrollmentsService : IEnrollmentsService
    {
        private readonly IEnrollmentsRepository _enrollmentsRepository;

        private readonly IStudentsRepository _studentsRepository;

        private readonly ICoursesRepository _coursesRepository;

        private readonly IMapper _mapper;

        public EnrollmentsService(IEnrollmentsRepository enrollmentsRepository, IStudentsRepository studentsRepository, ICoursesRepository coursesRepository, IMapper mapper)
        {
            _enrollmentsRepository = enrollmentsRepository;

            _studentsRepository = studentsRepository;

            _coursesRepository = coursesRepository;

            _mapper = mapper;
        }

        public Enrollment Get(int id)
        {
            var enrollmentEntity = _enrollmentsRepository.Get(id);

            if (enrollmentEntity == null)
            {
                throw new NotFoundException($"Enrollment with id {id} was not found");
            }

            return _mapper.Map<Enrollment>(enrollmentEntity);
        }

        public Enrollment Add(Enrollment enrollment)
        {
            ValidateEnrollment(enrollment);

            var enrollmentEntity = _mapper.Map<EnrollmentEntity>(enrollment);

            _enrollmentsRepository.Add(enrollmentEntity);

            _enrollmentsRepository.Save("Enrollment");

            return Get(enrollmentEntity.EnrollmentId);
        }

        public IEnumerable<Enrollment> AddRange(IEnumerable<Enrollment> enrollments)
        {
            if (enrollments == null)
            {
                throw new InvalidEnrollmentException("Enrollments must be provided");
            }

            foreach (var enrollment in enrollments)
            {
                ValidateEnrollment(enrollment);
            }

            var enrollmentEnitities = _mapper.Map<IEnumerable<EnrollmentEntity>>(enrollments).ToList();

            if (!enrollmentEnitities.Any()) return _mapper.Map<IEnumerable<Enrollment>>(enrollmentEnitities);

            _enrollmentsRepository.AddRange(enrollmentEnitities);

            _enrollmentsRepository.Save("Enrollments");

            return _mapper.Map<IEnumerable<Enrollment>>(enrollmentEnitities);
        }

        public IEnumerable<Enrollment> Update(int studentId, List<int> courseIds)
        {
            var newCourseIds = courseIds ?? new List<int>();

            var existingCourseIds = _enrollmentsRepository.Find(e => e.StudentId == studentId)
                .Select(x => x.CourseId).ToList();

            var dbCourseIds = _coursesRepository.GetAll().Select(c => c.CourseId);

            foreach (var dbCourseId in dbCourseIds)
            {
                if (newCourseIds.Contains(dbCourseId))
                {
                    if (existingCourseIds.Contains(dbCourseId)) continue;

                    var newEnrollment = new EnrollmentEntity
                    {
                        CourseId = dbCourseId,
                        StudentId = studentId
                    };

                    _enrollmentsRepository.Add(newEnrollment);
                }
                else
                {
                    if (!existingCourseIds.Contains(dbCourseId)) continue;

                    var removedEnrollment = _enrollmentsRepository.Find(e => e.CourseId == dbCourseId && e.StudentId == studentId).First();

                    _enrollmentsRepository.Remove(removedEnrollment);
                }
            }

            _enrollmentsRepository.Save("Enrollments");

            return _mapper.Map<IEnumerable<Enrollment>>(_enrollmentsRepository.Find(e => e.StudentId == studentId));
        }

        public bool Remove(int enrollmentId)
        {
            var enrollment = Get(enrollmentId);

            var enrollmentEntity = _mapper.Map<EnrollmentEntity>(enrollment);

            _enrollmentsRepository.Remove(enrollmentEntity);

            _enrollmentsRepository.Save("Enrollment");

            return true;
        }

        public bool RemoveRange(IEnumerable<Enrollment> enrollments)
        {
            var enrollmentEntities = _mapper.Map<IEnumerable<EnrollmentEntity>>(enrollments);

            _enrollmentsRepository.RemoveRange(enrollmentEntities);

            _enrollmentsRepository.Save("Enrollments");

            return true;
        }

        public void ValidateEnrollment(Enrollment enrollment)
        {
            if (enrollment == null)
            {
                throw new InvalidEnrollmentException("Enrollment must be provided");
            }

            if (enrollment.EnrollmentId != 0)
            {
                throw new InvalidEnrollmentException("Enrollment Id must be 0");
            }

            if (enrollment.StudentId == 0 || enrollment.CourseId == 0)
            {
                throw new InvalidEnrollmentException("Student and course must be provided");
            }

            ValidateStudent(enrollment);

            ValidateCourse(enrollment);
        }

        private void ValidateStudent(Enrollment enrollment)
        {
            var students = _studentsRepository.GetAll().ToList();

            var isStudentExisting = students.Any(s => s.StudentId == enrollment.StudentId);

            if (!isStudentExisting)
            {
                throw new InvalidEnrollmentException($"Student provided with Id {enrollment.StudentId} doesnot exist in the database");
            }
        }

        private void ValidateCourse(Enrollment enrollment)
        {
            var courses = _coursesRepository.GetAll().ToList();

            var isCourseExisting = courses.Any(c => c.CourseId == enrollment.CourseId);

            if (!isCourseExisting)
            {
                throw new InvalidEnrollmentException($"Course provided with id {enrollment.CourseId} doesnot exist in the database");
            }
        }
    }
}

