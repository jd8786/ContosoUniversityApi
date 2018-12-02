using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Api.Validators;

namespace ContosoUniversity.Api.Services
{
    public class EnrollmentsService : IEnrollmentsService
    {
        private readonly IEnrollmentsRepository _enrollmentsRepository;

        private readonly ICoursesRepository _coursesRepository;

        private readonly IEnrollmentValidator _enrollmentValidator;

        private readonly IMapper _mapper;

        public EnrollmentsService(IEnrollmentsRepository enrollmentsRepository, ICoursesRepository coursesRepository, IEnrollmentValidator enrollmentValidator, IMapper mapper)
        {
            _enrollmentsRepository = enrollmentsRepository;

            _coursesRepository = coursesRepository;

            _enrollmentValidator = enrollmentValidator;

            _mapper = mapper;
        }

        public Enrollment Get(int id)
        {
            var enrollmentEntity = _enrollmentsRepository.GetAll().FirstOrDefault(e => e.EnrollmentId == id);

            if (enrollmentEntity == null)
            {
                throw new NotFoundException($"Enrollment with id {id} was not found");
            }

            return _mapper.Map<Enrollment>(enrollmentEntity);
        }

        public Enrollment Add(Enrollment enrollment)
        {
            _enrollmentValidator.ValidatePostEnrollment(enrollment);

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
                _enrollmentValidator.ValidatePostEnrollment(enrollment);
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

            var existingCourseIds = _enrollmentsRepository.GetAll().Where(e => e.StudentId == studentId)
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

                    var removedEnrollment = _enrollmentsRepository.GetAll().First(e => e.CourseId == dbCourseId && e.StudentId == studentId);

                    _enrollmentsRepository.Remove(removedEnrollment);
                }
            }

            _enrollmentsRepository.Save("Enrollments");

            return _mapper.Map<IEnumerable<Enrollment>>(_enrollmentsRepository.GetAll().Where(e => e.StudentId == studentId));
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
    }
}

