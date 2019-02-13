using ContosoUniversity.Api.Models;
using ContosoUniversity.Data.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Api.Validators
{
    public class CommonValidator : ICommonValidator
    {
        public IIdValidator IdValidator { get; set; }

        public CommonValidator(IIdValidator idValidator)
        {
            IdValidator = idValidator;
        }

        public void ValidateCourseChildren(Course course)
        {
            if (course.Department == null)
            {
                throw new InvalidCourseException("Department must be provided");
            }

            IdValidator.ValidateDepartmentById(course.Department.DepartmentId);

            if (course.Students != null && course.Students.Any())
            {
                ValidateStudents(course.Students.ToList());
            }

            if (course.Instructors != null && course.Instructors.Any())
            {
                ValidateInstructors(course.Instructors.ToList());
            }
        }

        public void ValidateStudentChildren(Student student)
        {
            if (student.Courses != null && student.Courses.Any())
            {
                ValidateCourses(student.Courses.ToList());
            }
        }

        private void ValidateStudents(List<Student> students)
        {
            students.ForEach(s => IdValidator.ValidateStudentById(s.StudentId));
        }

        private void ValidateCourses(List<Course> courses)
        {
            courses.ForEach(c => IdValidator.ValidateCourseById(c.CourseId));
        }

        private void ValidateInstructors(List<Instructor> instructors)
        {
            instructors.ForEach(i => IdValidator.ValidateInstructorById(i.InstructorId));
        }
    }
}
