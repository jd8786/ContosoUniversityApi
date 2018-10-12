using ContosoUniversity.Data.EntityModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ContosoUniversity.Api.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Grade? Grade { get; set; }

        public Student Student { get; set; }

        public Course Course { get; set; }
    }
}
