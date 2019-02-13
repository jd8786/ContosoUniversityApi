using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class CourseRepository: BaseRepository<CourseEntity>, ICourseRepository
    {
        public CourseRepository(SchoolContext context) : base(context)
        {
        }

        public override IEnumerable<CourseEntity> GetAll()
        {
            return Context.Courses
                .Include(c => c.Enrollments)
                .ThenInclude(c => c.Student)
                .Include(c => c.Department)
                .Include(c => c.CourseAssignments)
                .ThenInclude(ca => ca.Instructor)
                .ThenInclude(i => i.OfficeAssignment)
                .ToList();
        }

        public override void Update(CourseEntity course)
        {
            var existingCourse = GetAll().First(s => s.CourseId == course.CourseId);

            Context.Entry(existingCourse).CurrentValues.SetValues(course);

            UpdateEnrollments(course, existingCourse);

            UpdateCourseAssignment(course, existingCourse);
        }

        private void UpdateEnrollments(CourseEntity course, CourseEntity existingCourse)
        {
            foreach (var enrollment in course.Enrollments ?? new List<EnrollmentEntity>())
            {
                var existingEnrollment = existingCourse.Enrollments.FirstOrDefault(e =>
                    e.CourseId == enrollment.CourseId && e.StudentId == enrollment.StudentId);

                if (existingEnrollment == null)
                {
                    existingCourse.Enrollments.Add(enrollment);
                }
            }

            foreach (var enrollment in existingCourse.Enrollments)
            {
                var shouldEnrollmentRemoved =
                    !course.Enrollments?.Any(e => e.CourseId == enrollment.CourseId && e.StudentId == enrollment.StudentId);

                if (shouldEnrollmentRemoved != false)
                {
                    Context.Remove(enrollment);
                }
            }
        }

        private void UpdateCourseAssignment(CourseEntity course, CourseEntity existingCourse)
        {
            foreach (var courseAssignment in course.CourseAssignments ?? new List<CourseAssignmentEntity>())
            {
                var existingCourseAssignment = existingCourse.CourseAssignments.FirstOrDefault(ca =>
                    ca.CourseId == courseAssignment.CourseId && ca.InstructorId == courseAssignment.InstructorId);

                if (existingCourseAssignment == null)
                {
                    existingCourse.CourseAssignments.Add(courseAssignment);
                }
            }

            foreach (var courseAssignment in existingCourse.CourseAssignments)
            {
                var shouldCourseAssignmentRemoved =
                    !course.CourseAssignments?.Any(e => e.CourseId == courseAssignment.CourseId && e.InstructorId == courseAssignment.InstructorId);

                if (shouldCourseAssignmentRemoved != false)
                {
                    Context.Remove(courseAssignment);
                }
            }
        }
    }
}
