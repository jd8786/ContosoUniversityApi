using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Data.Repositories
{
    public class StudentRepository : BaseRepository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(SchoolContext context) : base(context)
        {
        }

        public override IEnumerable<StudentEntity> GetAll()
        {
            return Context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course);
        }

        public override void Update(StudentEntity student)
        {
            var existingStudent = GetAll().First(s => s.StudentId == student.StudentId);

            Context.Entry(existingStudent).CurrentValues.SetValues(student);

            UpdateEnrollments(student, existingStudent);
        }

        private void UpdateEnrollments(StudentEntity student, StudentEntity existingStudent)
        {
            foreach (var enrollment in student.Enrollments ?? new List<EnrollmentEntity>())
            {
                var existingEnrollment = existingStudent.Enrollments.FirstOrDefault(e =>
                    e.CourseId == enrollment.CourseId && e.StudentId == enrollment.StudentId);

                if (existingEnrollment == null)
                {
                    existingStudent.Enrollments.Add(enrollment);
                }
                else
                {
                    existingEnrollment.Grade = enrollment.Grade;
                }
            }

            foreach (var enrollment in existingStudent.Enrollments)
            {
                var shouldEnrollmentRemoved =
                    !student.Enrollments?.Any(e => e.CourseId == enrollment.CourseId && e.StudentId == enrollment.StudentId);

                if (shouldEnrollmentRemoved != false)
                {
                    Context.Remove(enrollment);
                }
            }
        }
    }
}
