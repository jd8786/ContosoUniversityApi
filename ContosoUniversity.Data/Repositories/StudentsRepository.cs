using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class StudentsRepository : BaseRepository<StudentEntity>, IStudentsRepository
    {
        public StudentsRepository(SchoolContext context) : base(context)
        {
        }

        public override IEnumerable<StudentEntity> GetAll()
        {
            return GetAllWithTracking().AsNoTracking().ToList();
        }

        public IQueryable<StudentEntity> GetAllWithTracking()
        {
            return Context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course);
        }

        public override void Update(StudentEntity student)
        {
            var existingStudent = GetAllWithTracking().First(s => s.StudentId == student.StudentId);

            Context.Entry(existingStudent).CurrentValues.SetValues(student);

            foreach (var enrollment in student.Enrollments)
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
                var isEnrollmentRemoved = !student.Enrollments.Any(e => e.CourseId == enrollment.CourseId && e.StudentId == enrollment.StudentId);

                if (isEnrollmentRemoved)
                {
                    Context.Remove(enrollment);
                }
            }
        }
    }
}
