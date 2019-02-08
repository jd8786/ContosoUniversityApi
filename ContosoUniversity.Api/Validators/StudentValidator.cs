using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Exceptions;
using System.Linq;

namespace ContosoUniversity.Api.Validators
{
    public class StudentValidator : IStudentValidator
    {
        public ICommonValidator CommonValidator { get; set; }

        public StudentValidator(ICommonValidator commonValidator)
        {
            CommonValidator = commonValidator;
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

            CommonValidator.ValidateStudentById(student.StudentId);

            ValidateChildren(student);
        }

        private void ValidateChildren(Student student)
        {
            if (student.Courses != null && student.Courses.Any())
            {
                student.Courses.ToList().ForEach(c => CommonValidator.ValidateCourseById(c.CourseId));
            }
        }
    }
}
