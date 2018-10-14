using System;
using System.Collections.Generic;
using System.Net;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using ContosoUniversity.Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;

namespace ContosoUniversity.Api.Controllers
{
    [Route("api/students")]
    public class StudentsController: Controller
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
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
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
                return NotFound(ApiResponse<bool>.Error($"{ex.Message}"));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostStudent([FromBody] Student student)
        {
            try
            {
                var newStudent = _service.Add(student);

                return Ok(ApiResponse<Student>.Success(newStudent));
            }
            catch (InvalidStudentException ex)
            {
                return BadRequest(ApiResponse<bool>.Error($"{ex.Message}"));
            }
            catch (InvalidCourseException ex)
            {
                return BadRequest(ApiResponse<bool>.Error($"{ex.Message}"));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponse<bool>.Error(ex.Message));
            }
        }

        //[HttpPut]
        //public async Task<IActionResult> PutStudentInfoAsync([FromBody] StudentInfo studentInfo)
        //{
        //    if (studentInfo == null)
        //    {
        //        return BadRequest(ApiResponseOfBoolean.Error("Bad Request"));
        //    }

        //    try
        //    {
        //        var isStudentInfoExist = await _service.UpdateStudentInfoAsync(studentInfo);

        //        if (!isStudentInfoExist)
        //        {
        //            return NotFound(ApiResponseOfBoolean.Error("Student Not Found"));
        //        }

        //        return Ok(ApiResponseOfBoolean.Success());
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponseOfBoolean.Error(ex.Message));
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStudentInfoAsync(int id)
        //{
        //    try
        //    {
        //        var isStudentInfoExist = await _service.DeleteStudentInfoAsync(id);

        //        if (!isStudentInfoExist)
        //        {
        //            return NotFound(ApiResponseOfBoolean.Error("Student Not Found"));
        //        }

        //        return Ok(ApiResponseOfBoolean.Success());
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError,
        //            ApiResponseOfBoolean.Error(ex.Message));
        //    }
        //}

    }
}
