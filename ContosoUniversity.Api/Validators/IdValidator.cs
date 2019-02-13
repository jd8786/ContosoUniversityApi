using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;

namespace ContosoUniversity.Api.Validators
{
    public class IdValidator : IIdValidator
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IInstructorRepository _instructorRepository;

        public IdValidator(IStudentRepository studentRepository, ICourseRepository courseRepository,
            IDepartmentRepository departmentRepository, IInstructorRepository instructorRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
            _instructorRepository = instructorRepository;
        }

        public void ValidateStudentById(int studentId)
        {
            var existingStudent = _studentRepository.Find(studentId);

            if (existingStudent == null)
            {
                throw new NotFoundException($"Student provided with Id {studentId} doesnot exist in the database");
            }
        }

        public void ValidateCourseById(int courseId)
        {
            var existingCourse = _courseRepository.Find(courseId);

            if (existingCourse == null)
            {
                throw new NotFoundException($"Course provided with Id {courseId} doesnot exist in the database");
            }
        }

        public void ValidateDepartmentById(int departmentId)
        {
            var existingDepartment = _departmentRepository.Find(departmentId);

            if (existingDepartment == null)
            {
                throw new NotFoundException($"Department provided with Id {departmentId} doesnot exist in the database");
            }
        }

        public void ValidateInstructorById(int instructorId)
        {
            var existingInstructor = _instructorRepository.Find(instructorId);

            if (existingInstructor == null)
            {
                throw new NotFoundException($"Instructor provided with Id {instructorId} doesnot exist in the database");
            }
        }
    }
}
