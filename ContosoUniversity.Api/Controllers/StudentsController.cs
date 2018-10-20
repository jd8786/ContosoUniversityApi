using System;
using System.Collections.Generic;
using System.Net;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using ContosoUniversity.Data.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Api.Controllers
{
    [Route("api/students")]
    public class StudentsController : Controller
    {
        private readonly IStudentsService _service;

        public StudentsController(IStudentsService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<Student>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult GetStudents()
        {
            try
            {
                var students = _service.GetAll();

                var apiResponse = ApiResponse<IEnumerable<Student>>.Success(students);

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Student>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult GetStudentById(int id)
        {
            try
            {
                var student = _service.Get(id);

                var apiResponse = ApiResponse<Student>.Success(student);

                return Ok(apiResponse);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<bool>.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Student>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 400)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult PostStudent([FromBody] Student student)
        {
            try
            {
                var newStudent = _service.Add(student);

                return Ok(ApiResponse<Student>.Success(newStudent));
            }
            catch (InvalidStudentException ex)
            {
                return BadRequest(ApiResponse<bool>.Error(ex.Message));
            }
            catch (InvalidEnrollmentException ex)
            {
                return BadRequest(ApiResponse<bool>.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponse<bool>.Error(ex.Message));
            }
        }

        [HttpPut]
        public IActionResult PutStudent([FromBody] Student student)
        {
            try
            {
                var updatedStudent = _service.Update(student);

                return Ok(ApiResponse<Student>.Success(updatedStudent));
            }
            catch (InvalidStudentException ex)
            {
                return BadRequest(ApiResponse<bool>.Error(ex.Message));
            }
            catch (InvalidEnrollmentException ex)
            {
                return BadRequest(ApiResponse<bool>.Error(ex.Message));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<bool>.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponse<bool>.Error(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _service.Remove(id);

                return Ok(ApiResponse<bool>.Success(true));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ApiResponse<bool>.Error(ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponse<bool>.Error(ex.Message));
            }
        }

    }
}
