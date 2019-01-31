using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class InstructorRepository : BaseRepository<InstructorEntity>, IInstructorRepository
    {
        public InstructorRepository(SchoolContext context) : base(context)
        {
        }

        public override IEnumerable<InstructorEntity> GetAll()
        {
            return Context.Instructors
                .Include(i => i.CourseAssignments)
                .ThenInclude(ca => ca.Course)
                .Include(i => i.OfficeAssignment);
        }

        public override void Update(InstructorEntity instructor)
        {
            var existingInstructor = GetAll().First(i => i.InstructorId == instructor.InstructorId);

            Context.Entry(existingInstructor).CurrentValues.SetValues(instructor);

            UpdateCourseAssignment(instructor, existingInstructor);
        }

        private void UpdateCourseAssignment(InstructorEntity instructor, InstructorEntity existingInstructor)
        {
            foreach (var courseAssignment in instructor.CourseAssignments ?? new List<CourseAssignmentEntity>())
            {
                var existingCourseAssignment = existingInstructor.CourseAssignments.FirstOrDefault(ca =>
                    ca.CourseId == courseAssignment.CourseId && ca.InstructorId == courseAssignment.InstructorId);

                if (existingCourseAssignment == null)
                {
                    existingInstructor.CourseAssignments.Add(courseAssignment);
                }
            }

            foreach (var courseAssignment in existingInstructor.CourseAssignments)
            {
                var shouldCourseAssignmentRemoved =
                    !instructor.CourseAssignments?.Any(e => e.CourseId == courseAssignment.CourseId && e.InstructorId == courseAssignment.InstructorId);

                if (shouldCourseAssignmentRemoved != false)
                {
                    Context.Remove(courseAssignment);
                }
            }
        }
    }
}
