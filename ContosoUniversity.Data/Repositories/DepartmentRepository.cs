using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class DepartmentRepository : BaseRepository<DepartmentEntity>, IDepartmentRepository
    {
        public DepartmentRepository(SchoolContext context) : base(context)
        {
        }

        public override IEnumerable<DepartmentEntity> GetAll()
        {
            return Context.Departments
                .Include(d => d.Administrator)
                .Include(d => d.Courses);
        }

        public override void Update(DepartmentEntity department)
        {
            var existingDepartment = GetAll().First(d => d.DepartmentId == department.DepartmentId);

            Context.Entry(existingDepartment).CurrentValues.SetValues(department);

            UpdateCourses(department, existingDepartment);
        }

        private void UpdateCourses(DepartmentEntity department, DepartmentEntity existingDepartment)
        {
            foreach (var course in department.Courses ?? new List<CourseEntity>())
            {
                var existingCourse = existingDepartment.Courses.FirstOrDefault(c => c.CourseId == course.CourseId);

                if (existingCourse == null)
                {
                    existingDepartment.Courses.Add(course);
                }
            }

            foreach (var course in existingDepartment.Courses)
            {
                var shouldCourseRemoved = !department.Courses?.Any(c => c.CourseId == course.CourseId);

                if (shouldCourseRemoved != false)
                {
                    Context.Remove(course);
                }
            }
        }
    }
}
