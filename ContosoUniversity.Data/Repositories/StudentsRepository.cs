using ContosoUniversity.Data.EntityModels;

namespace ContosoUniversity.Data.Repositories
{
    public class StudentsRepository: BaseRepository<StudentEntity>, IStudentsRepository
    {
        public StudentsRepository(SchoolContext context) : base(context)
        {
        }

        //public override void Add(StudentEntity entity)
        //{
        //    var courseIds = new List<int>();

        //    if (entity.Enrollments != null && entity.Enrollments.Any())
        //    {
        //        courseIds = entity.Enrollments.Select(e => e.CourseId).ToList();
        //    }

        //    entity.Enrollments = null;

        //    _context.Set<StudentEntity>().Add(entity);

        //    Save("Student");

        //    if (!courseIds.Any()) return;

        //    AddEnrollments(entity, courseIds);
        //}

        //private void AddEnrollments(StudentEntity entity, List<int> courseIds)
        //{
        //    var enrollments = new List<EnrollmentEntity>();

        //    foreach (var courseId in courseIds)
        //    {
        //        var enrollment = new EnrollmentEntity
        //        {
        //            StudentId = entity.StudentId,
        //            CourseId = courseId
        //        };

        //        enrollments.Add(enrollment);
        //    }

        //    _context.Set<EnrollmentEntity>().AddRange(enrollments);

        //    Save("Enrollments");
        //}
    }
}
