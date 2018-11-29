using ContosoUniversity.Data.Exceptions;
using ContosoUniversity.Data.Repositories;
using System.Linq;

namespace ContosoUniversity.Api.Validators
{
    public class StudentValidator: IStudentValidator
    {
        private readonly IStudentsRepository _studentsRepository;

        public StudentValidator(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }

        public void Validate(int studentId)
        {
            var students = _studentsRepository.GetAll().ToList();

            var isStudentExisting = students.Any(s => s.StudentId == studentId);

            if (!isStudentExisting)
            {
                throw new InvalidStudentException($"Student provided with Id {studentId} doesnot exist in the database");
            }
        }
    }
}
