using System;
using System.Net;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
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
        public IActionResult GetStudentInfos()
        {
            try
            {
                var studentInfos = _service.GetStudentInfos();

                var apiResponseOfStudentInfos = ApiResponseOfStudentInfos.Success(studentInfos);

                return Ok(apiResponseOfStudentInfos);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        [HttpGet("{id}", Name = "GetStudentInfoById")]
        public IActionResult GetStudentInfoById(int id)
        {
            try
            {
                var studentInfo = _service.GetStudentInfoById(id);

                if (studentInfo == null)
                {
                    return NotFound(ApiResponseOfBoolean.Error("Student Not Found"));
                }

                var apiResponseOfStudentInfo = ApiResponseOfStudentInfo.Success(studentInfo);

                return Ok(apiResponseOfStudentInfo);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateStudentInfo([FromBody] StudentInfo studentInfo)
        {
            if (studentInfo == null)
            {
                return BadRequest(ApiResponseOfBoolean.Error("Bad Request"));
            }

            try
            {
                _service.CreateStudentInfo(studentInfo);

                return CreatedAtRoute("GetStudentInfoById", new { id = studentInfo.StudentInfoId }, studentInfo);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponseOfBoolean.Error(ex.Message));
            }
        }

        [HttpPut]
        public IActionResult UpdateStudentInfo([FromBody] StudentInfo studentInfo)
        {
            if (studentInfo == null)
            {
                return BadRequest(ApiResponseOfBoolean.Error("Bad Request"));
            }

            try
            {
                var isStudentInfoExist = _service.UpdateStudentInfo(studentInfo);

                if (!isStudentInfoExist)
                {
                    return NotFound(ApiResponseOfBoolean.Error("Student Not Found"));
                }

                return Ok(ApiResponseOfBoolean.Success());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponseOfBoolean.Error(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudentInfo(int id)
        {
            try
            {
                var isStudentInfoExist = _service.DeleteStudentInfo(id);

                if (!isStudentInfoExist)
                {
                    return NotFound(ApiResponseOfBoolean.Error("Student Not Found"));
                }

                return Ok(ApiResponseOfBoolean.Success());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    ApiResponseOfBoolean.Error(ex.Message));
            }
        }

    }
}
