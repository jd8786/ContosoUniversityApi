using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using System.Linq;
using ContosoUniversity.Api.Models;

namespace ContosoUniversity.Api.Validators
{
    public class StudentValidator: IStudentValidator
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseValidator _courseValidator;

        public StudentValidator(IStudentRepository studentRepository, ICourseValidator courseValidator)
        {
            _studentRepository = studentRepository;
            _courseValidator = courseValidator;
        }

        public void ValidateById(int studentId)
        {
            var students = _studentRepository.GetAll().ToList();

            var isStudentExisting = students.Any(s => s.StudentId == studentId);

            if (!isStudentExisting)
            {
                throw new NotFoundException($"Student provided with Id {studentId} doesnot exist in the database");
            }
        }

        public void ValidatePostStudent(Student student)
        {
            if (student == null)
            {
                throw new InvalidStudentException("Student must be provided");
            }

            if (student.StudentId != 0)
            {
                throw new InvalidStudentException("Student Id must be 0");
            }

            ValidateChildren(student);
        }

        public void ValidatePutStudent(Student student)
        {
            if (student == null)
            {
                throw new InvalidStudentException("Student must be provided");
            }

            if (student.StudentId == 0)
            {
                throw new InvalidStudentException("Student Id cannot be 0");
            }

            ValidateById(student.StudentId);

            ValidateChildren(student);
        }

        private void ValidateChildren(Student student)
        {
            if (student.Courses != null && student.Courses.Any())
            {
                student.Courses.ToList().ForEach(c => _courseValidator.ValidateById(c.CourseId));
            }
        }
    }
}
