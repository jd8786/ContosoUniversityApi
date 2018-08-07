using System;
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

        public void CreateStudent(Student student)
        {
            _context.Students.Add(student);

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                var studentName = $"{student?.FirstMidName} {student?.LastName}".Trim() ?? string.Empty;

                throw new Exception($"Student {studentName} was failed to be saved in the database");
            }
        }

        public bool UpdateStudent(Student student)
        {
            var currrentStudent = GetStudentById(student.StudentId);

            if (currrentStudent == null)
            {
                return false;
            }

            currrentStudent.FirstMidName = student.FirstMidName;

            currrentStudent.LastName = student.LastName;

            currrentStudent.EnrollmentDate = student.EnrollmentDate;

            _context.Students.Update(currrentStudent);

            try
            {
                _context.SaveChanges();
            }
            catch 
            {
                var studentName = $"{currrentStudent.FirstMidName} {currrentStudent.LastName}".Trim() ?? string.Empty;

                throw new Exception($"Student {studentName} was failed to be updated in the database");
            }

            return true;
        }

        public bool DeleteStudent(int studentId)
        {
            var student = GetStudentById(studentId);

            if (student == null)
            {
                return false;
            }

            _context.Students.Remove(student);

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                var studentName = $"{student.FirstMidName} {student.LastName}".Trim() ?? string.Empty;

                throw new Exception($"Student {studentName} was failed to be removed in the database");
            }

            return true;
        }
    }
}
