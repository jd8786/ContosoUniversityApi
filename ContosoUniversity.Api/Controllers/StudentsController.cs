using System;
using System.Collections.Generic;
using System.Net;
using Castle.Core.Internal;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using ContosoUniversity.Data.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Api.Controllers
{
    [Route("api/students")]
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;

        private readonly IEnrollmentsService _enrollmentsService;

        public StudentsController(IStudentsService studentsService, IEnrollmentsService enrollmentsService)
        {
            _studentsService = studentsService;
            _enrollmentsService = enrollmentsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<Student>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult GetStudents()
        {
            try
            {
                var students = _studentsService.GetAll();

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
                var student = _studentsService.Get(id);

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
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult PostStudent([FromBody] Student student)
        {
            try
            {
                var addedStudent = _studentsService.Add(student);

                if (!student.Enrollments.IsNullOrEmpty())
                {
                    foreach (var studentEnrollment in student.Enrollments)
                    {
                        studentEnrollment.StudentId = addedStudent.StudentId;
                    }

                    _enrollmentsService.AddRange(student.Enrollments);
                }

                var newStudent = _studentsService.Get(addedStudent.StudentId);

                return Ok(ApiResponse<Student>.Success(newStudent));
            }
            catch (InvalidStudentException ex)
            {
                return BadRequest(ApiResponse<bool>.Error(ex.Message));
            }
            catch (InvalidCourseException ex)
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

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<Student>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 400)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult PutStudent([FromBody] Student student)
        {
            try
            {
                var updatedStudent = _studentsService.Update(student);

                _enrollmentsService.Update(student.StudentId, student.Enrollments);

                return Ok(ApiResponse<Student>.Success(updatedStudent));
            }
            catch (InvalidStudentException ex)
            {
                return BadRequest(ApiResponse<bool>.Error(ex.Message));
            }
            catch (InvalidCourseException ex)
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
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                _studentsService.Remove(id);

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
