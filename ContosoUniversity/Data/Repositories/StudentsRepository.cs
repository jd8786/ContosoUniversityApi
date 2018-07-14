using System.Collections.Generic;
using System.Linq;
using ContosoUniversity.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class StudentsRepository: IStudentsRepository
    {
        private readonly SchoolContext _context;

        public StudentsRepository(SchoolContext context)
        {
            _context = context;
        }
        public IEnumerable<Student> GetStudents()
        {
            var students = _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToList();

            return students;
        }

        public Student GetStudentById(int studentId)
        {
            var student = GetStudents().FirstOrDefault(s => s.StudentId == studentId);

            return student;
        }
    }
}
