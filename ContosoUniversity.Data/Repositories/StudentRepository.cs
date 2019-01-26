using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

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
                .ThenInclude(e => e.Course)
                .Include(s => s.Enrollments)
                .ThenInclude(s => s.Student);
        }

        public override void Update(StudentEntity student)
        {
            var existingStudent = GetAll().First(s => s.StudentId == student.StudentId);

            Context.Entry(existingStudent).CurrentValues.SetValues(student);

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
                var shouldEnrollmentRemoved = !student.Enrollments?.Any(e => e.CourseId == enrollment.CourseId && e.StudentId == enrollment.StudentId);

                if (shouldEnrollmentRemoved != false)
                {
                    Context.Remove(enrollment);
                }
            }
        }
    }
}
