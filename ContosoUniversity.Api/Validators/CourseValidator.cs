using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using System.Linq;

namespace ContosoUniversity.Api.Validators
{
    public class CourseValidator: ICourseValidator
    {
        private readonly ICoursesRepository _coursesRepository;

        public CourseValidator(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public void Validate(int courseId)
        {
            var courses = _coursesRepository.GetAll().ToList();

            var isCourseExisting = courses.Any(c => c.CourseId == courseId);

            if (!isCourseExisting)
            {
                throw new InvalidCourseException($"Course provided with id {courseId} doesnot exist in the database");
            }
        }
    }
}
