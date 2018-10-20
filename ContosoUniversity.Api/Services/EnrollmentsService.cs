using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;

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

            var validEnrollments = new List<Enrollment>();

            var exceptions = new List<InvalidEnrollmentException>();

            foreach (var enrollment in enrollments)
            {
                try
                {
                    ValidateEnrollment(enrollment);

                    validEnrollments.Add(enrollment);
                }
                catch (InvalidEnrollmentException ex)
                {
                    exceptions.Add(ex);
                }
            }

            var validEnrollmentEnitities = new List<EnrollmentEntity>();

            if (validEnrollments.Any())
            {
                validEnrollmentEnitities = _mapper.Map<IEnumerable<EnrollmentEntity>>(validEnrollments).ToList();

                _enrollmentsRepository.AddRange(validEnrollmentEnitities);

                _enrollmentsRepository.Save("Enrollments");
            }

            if (exceptions.Any())
            {
                ThrowExceptionWithNewMessage(validEnrollmentEnitities, exceptions);
            }

            return _mapper.Map<IEnumerable<Enrollment>>(validEnrollmentEnitities);
        }

        public bool Remove(int enrollmentId)
        {
            var enrollment = Get(enrollmentId);

            ValidateEnrollment(enrollment);

            var enrollmentEntity = _mapper.Map<EnrollmentEntity>(enrollment);

            _enrollmentsRepository.Remove(enrollmentEntity);

            _enrollmentsRepository.Save("Enrollment");

            return true;
        }

        private static void ThrowExceptionWithNewMessage(List<EnrollmentEntity> validEnrollmentEnitities, List<InvalidEnrollmentException> exceptions)
        {
            var idMessage = string.Empty;

            if (validEnrollmentEnitities.Any())
            {
                var enrollmentIds = validEnrollmentEnitities.Select(ve => ve.EnrollmentId).ToList();

                idMessage = enrollmentIds.Any()
                    ? $"; enrollment(s) with id {string.Join(",", enrollmentIds)} was(were) saved to database"
                    : string.Empty;
            }

            var exceptionMessages = exceptions.Select(e => e.Message);

            var joinedExceptionMessage = string.Join(",", exceptionMessages);

            throw new InvalidEnrollmentException($"{joinedExceptionMessage}{idMessage}");
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
