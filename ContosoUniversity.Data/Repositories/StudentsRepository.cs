using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoUniversity.Data.EntityModels;
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
        public async Task<IEnumerable<StudentEntity>> GetStudentsAsync()
        {
            var students = _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToListAsync();

            return await students;
        }

        public async Task<StudentEntity> GetStudentByIdAsync(int studentId)
        {
            var student = _context.Students.FirstOrDefaultAsync(s => s.StudentId == studentId);

            return await student;
        }

        public async Task<StudentEntity> CreateAsync(StudentEntity student)
        {
            await _context.Students.AddAsync(student);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Student was failed to be saved in the database");
            }

            return student;
        }

        public async Task<bool> UpdateAsync(StudentEntity student)
        {
            var currrentStudent = await GetStudentByIdAsync(student.StudentId);

            if (currrentStudent == null)
            {
                return false;
            }

            currrentStudent.FirstMidName = student.FirstMidName;

            currrentStudent.LastName = student.LastName;

            currrentStudent.EnrollmentDate = student.EnrollmentDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch 
            {
                throw new Exception("Student was failed to be updated in the database");
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int studentId)
        {
            var student = await GetStudentByIdAsync(studentId);

            if (student == null)
            {
                return false;
            }

            _context.Students.Remove(student);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Student was failed to be removed in the database");
            }

            return true;
        }
    }
}
