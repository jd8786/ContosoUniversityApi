using ContosoUniversity.Api.Models;
using System.Collections.Generic;

namespace ContosoUniversity.Api.Services
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAll();

        Course Get(int id);

        Course Add(Course course);

        Course Update(Course course);

        bool Remove(int courseId);
    }
}
