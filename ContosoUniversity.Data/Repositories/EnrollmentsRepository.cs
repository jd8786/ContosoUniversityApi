using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using ContosoUniversity.Data.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class EnrollmentsRepository: BaseRepository<EnrollmentEntity>, IEnrollmentsRepository
    {
        public EnrollmentsRepository(SchoolContext context) : base(context)
        {
        }

        public override IEnumerable<EnrollmentEntity> GetAll()
        {
            return Context.Enrollments.AsNoTracking()
                .Include(e => e.Course)
                .Include(e => e.Student)
                .ToList();
        }

        public IEnumerable<EnrollmentEntity> GetByStudentId(int studentId)
        {
            return GetAll().Where(e => e.StudentId == studentId);
        }

        public void UpdateEnrollmentGrade(int studentId, int courseId, EnrollmentEntity enrollmentEntity)
        {
            var existingEnrollment = GetAll().First(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrollmentEntity.EnrollmentId != existingEnrollment.EnrollmentId)
            {
                throw new InvalidEnrollmentException("Enrollment Id is invalid");
            }

            if (existingEnrollment.Grade == enrollmentEntity.Grade) return;

            existingEnrollment.Grade = enrollmentEntity.Grade;

            Update(existingEnrollment);
        }
    }
}
