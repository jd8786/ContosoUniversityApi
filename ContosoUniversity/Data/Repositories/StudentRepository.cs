using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Data.Repositories
{
    public class StudentRepository: IStudentRepository
    {
        public readonly SchoolContext _context;

        public StudentRepository(SchoolContext context)
        {
            _context = context;
        }
        public IEnumerable<Student> GetStudents()
        {
            var students = _context.Students.Include(s => s.Enrollments)
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
