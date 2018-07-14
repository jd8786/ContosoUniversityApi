using System;
using System.Net;
using ContosoUniversity.Data.Models;
using ContosoUniversity.Data.Repositories;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Controllers
{
    [Route("api/[controller]")]
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
            try
            {
                var students = _repository.GetStudents();

                var apiResponseOfStudents = ApiResponseOfStudents.Success(students);

                return Ok(apiResponseOfStudents);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        [HttpGet("{id}", Name = "GetStudentById")]
        public IActionResult GetStudentById(int id)
        {
            try
            {
                var student = _repository.GetStudentById(id);

                if (student == null)
                {
                    return NotFound(ApiResponseOfStudent.Error(null, "Studnent Not Found"));
                }

                var apiResponseOfStudent = ApiResponseOfStudent.Success(student);

                return Ok(apiResponseOfStudent);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest(ApiResponseBase.Error("Bad Request"));
            }

            try
            {
                _repository.CreateStudent(student);
                return CreatedAtRoute("GetStudentById", new { id = student.StudentId }, student);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponseBase.Error(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            if (student == null || student.StudentId != id)
            {
                return BadRequest(ApiResponseBase.Error("Bad Request"));
            }

            try
            {
                var isStudentExist = _repository.UpdateStudent(id, student);

                if (!isStudentExist)
                {
                    return NotFound(ApiResponseBase.Error("Student Not Found"));
                }

                return Ok(ApiResponseBase.Success());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponseBase.Error(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                var isStudentExist = _repository.DeleteStudent(id);

                if (!isStudentExist)
                {
                    return NotFound(ApiResponseBase.Error("Student Not Found"));
                }

                return Ok(ApiResponseBase.Success());
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError,
                    ApiResponseBase.Error(ex.Message));
            }
        }

    }
}
