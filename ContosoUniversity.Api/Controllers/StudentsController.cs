using System;
using System.Net;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using ContosoUniversity.Data.Models;
using ContosoUniversity.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Api.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController: Controller
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            try
            {
                var students = _service.GetStudents();

                var apiResponseOfStudents = ApiResponseOfStudents.Success(students);

                return Ok(apiResponseOfStudents);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        //[HttpGet("{id}", Name = "GetStudentById")]
        //public IActionResult GetStudentById(int id)
        //{
        //    try
        //    {
        //        var student = _service.GetStudentById(id);

        //        if (student == null)
        //        {
        //            return NotFound(ApiResponseOfBoolean.Error("Student Not Found"));
        //        }

        //        var apiResponseOfStudent = ApiResponseOfStudent.Success(student);

        //        return Ok(apiResponseOfStudent);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //}

        //[HttpPost]
        //public IActionResult CreateStudent([FromBody] Student student)
        //{
        //    if (student == null)
        //    {
        //        return BadRequest(ApiResponseOfBoolean.Error("Bad Request"));
        //    }

        //    try
        //    {
        //        _service.CreateStudent(student);
        //        return CreatedAtRoute("GetStudentById", new { id = student.StudentId }, student);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponseOfBoolean.Error(ex.Message));
        //    }
        //}

        //[HttpPut]
        //public IActionResult UpdateStudent([FromBody] Student student)
        //{
        //    if (student == null)
        //    {
        //        return BadRequest(ApiResponseOfBoolean.Error("Bad Request"));
        //    }

        //    try
        //    {
        //        var isStudentExist = _service.UpdateStudent(student);

        //        if (!isStudentExist)
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
        //public IActionResult DeleteStudent(int id)
        //{
        //    try
        //    {
        //        var isStudentExist = _service.DeleteStudent(id);

        //        if (!isStudentExist)
        //        {
        //            return NotFound(ApiResponseOfBoolean.Error("Student Not Found"));
        //        }

        //        return Ok(ApiResponseOfBoolean.Success());
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int) HttpStatusCode.InternalServerError,
        //            ApiResponseOfBoolean.Error(ex.Message));
        //    }
        //}

    }
}
