﻿using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using ContosoUniversity.Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace ContosoUniversity.Api.Controllers
{
    [Route("api/students")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<Student>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult GetStudents()
        {
            try
            {
                var students = _studentService.GetAll();

                var apiResponse = ApiResponse<IEnumerable<Student>>.Success(students);

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponse<bool>.Error(ex.Message));
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
                var student = _studentService.Get(id);

                var apiResponse = ApiResponse<Student>.Success(student);

                return Ok(apiResponse);
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

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Student>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 400)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult PostStudent([FromBody] Student student)
        {
            try
            {
                var newStudent = _studentService.Add(student);

                return Ok(ApiResponse<Student>.Success(newStudent));
            }
            catch (InvalidStudentException ex)
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
                var updatedStudent = _studentService.Update(student);

                return Ok(ApiResponse<Student>.Success(updatedStudent));
            }
            catch (InvalidStudentException ex)
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
                _studentService.Remove(id);

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
