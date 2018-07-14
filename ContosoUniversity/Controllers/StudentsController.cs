using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    [Route("api/students")]
    public class StudentsController: Controller
    {
        private readonly IStudentsRepository _repository;

        public StudentsController(IStudentsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _repository.GetStudents();

            var apiResponseOfStudents = ApiResponseOfStudents.Success(students);

            return Ok(apiResponseOfStudents);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _repository.GetStudentById(id);

            if (student == null)
            {
                return NotFound(ApiResponseOfStudent.Error(null, "Studnent Not Found"));
            }

            var apiResponseOfStudent = ApiResponseOfStudent.Success(student);

            return Ok(apiResponseOfStudent);
        }

    }
}
