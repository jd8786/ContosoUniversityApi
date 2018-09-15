using ContosoUniversity.Data.Models;

namespace ContosoUniversity.Api.Models
{
    public class CourseInfo
    {
        public int CourseInfoId { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public Grade? Grade { get; set; }
    }
}
