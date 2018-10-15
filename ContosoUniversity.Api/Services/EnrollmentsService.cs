using System.Linq;
using AutoMapper;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;

namespace ContosoUniversity.Api.Services
{
    public class EnrollmentsService: IEnrollmentsService
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
                throw new NotFoundException("Enrollment was not found");
            }

            return _mapper.Map<Enrollment>(enrollmentEntity);
        }

        public Enrollment Add(Enrollment enrollment)
        {
            if (enrollment == null)
            {
                throw new InvalidEnrollmentException("Enrollment must be provided");
            }

            if (enrollment.StudentId == 0 || enrollment.CourseId == 0)
            {
                throw new InvalidEnrollmentException("Student and course must be provided");
            }

            var students = _studentsRepository.GetAll().ToList();

            var courses = _coursesRepository.GetAll().ToList();

            foreach (var student in students)
            {
                var isStudentExisting = students.Any(s => s.StudentId == student.StudentId);

                if (!isStudentExisting)
                {
                    throw new InvalidStudentException("Student provided doesnot exist in the database");
                }
            }

            foreach (var course in courses)
            {
                var isCourseExisting = courses.Any(c => c.CourseId == course.CourseId);

                if (!isCourseExisting)
                {
                    throw new InvalidCourseException("Course provided doesnot exist in the database");
                }
            }

            var enrollmentEntity = _mapper.Map<EnrollmentEntity>(enrollment);

            _enrollmentsRepository.Add(enrollmentEntity);

            _enrollmentsRepository.Save("Enrollment");

            return Get(enrollmentEntity.EnrollmentId);
        }
    }
}
