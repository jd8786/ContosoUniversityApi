using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
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

        public IEnumerable<Enrollment> AddRange(IEnumerable<Enrollment> enrollments)
        {
            if (enrollments == null)
            {
                throw new InvalidEnrollmentException("Enrollments must be provided");
            }

            if (!enrollments.Any()) return new List<Enrollment>();

            foreach (var enrollment in enrollments)
            {
                _enrollmentValidator.ValidatePostEnrollment(enrollment);
            }

            var enrollmentEnitities = _mapper.Map<IEnumerable<EnrollmentEntity>>(enrollments);

            _enrollmentsRepository.AddRange(enrollmentEnitities);

            _enrollmentsRepository.Save("Enrollments");

            return _mapper.Map<IEnumerable<Enrollment>>(enrollmentEnitities);
        }

        public IEnumerable<Enrollment> Update(int studentId, IEnumerable<Enrollment> enrollments)
        {
            if (!enrollments.IsNullOrEmpty())
            {
                enrollments.ToList().ForEach(e => _enrollmentValidator.ValidatePutEnrollment(e));
            }

            var newCourseIds = enrollments == null ? new List<int>() : enrollments.Select(e => e.CourseId).ToList(); 

            var existingCourseIds = _enrollmentsRepository.GetAll().Where(e => e.StudentId == studentId)
                .Select(x => x.CourseId).ToList();

            var dbCourseIds = _coursesRepository.GetAll().Select(c => c.CourseId);

            foreach (var dbCourseId in dbCourseIds)
            {
                if (newCourseIds.Contains(dbCourseId))
                {
                    var addedEnrollment =
                        enrollments.First(e => e.StudentId == studentId && e.CourseId == dbCourseId);

                    if (existingCourseIds.Contains(dbCourseId))
                    {
                        _enrollmentsRepository.UpdateEnrollmentGrade(studentId, dbCourseId, _mapper.Map<EnrollmentEntity>(addedEnrollment));

                        continue;
                    }

                    if (addedEnrollment.EnrollmentId != 0)
                    {
                        throw new InvalidEnrollmentException("Enrollment Id must be 0");
                    }

                    _enrollmentsRepository.Add(_mapper.Map<EnrollmentEntity>(addedEnrollment));
                }
                else
                {
                    if (!existingCourseIds.Contains(dbCourseId)) continue;

                    var removedEnrollment = _enrollmentsRepository.GetAll().First(e => e.CourseId == dbCourseId && e.StudentId == studentId);

                    _enrollmentsRepository.Remove(removedEnrollment);
                }
            }

            _enrollmentsRepository.Save("Enrollments");

            var updatedEnrollmentEntities = _enrollmentsRepository.GetAll().Where(e => e.StudentId == studentId);

            return _mapper.Map<IEnumerable<Enrollment>>(updatedEnrollmentEntities);
        }
    }
}

