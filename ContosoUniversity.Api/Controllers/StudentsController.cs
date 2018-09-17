using System;
using System.Net;
using System.Threading.Tasks;
using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniversity.Api.Controllers
{
    [Route("api/students")]
    public class StudentsController: Controller
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentInfosAsync()
        {
            try
            {
                var studentInfos = await _service.GetStudentInfosAsync();

                var apiResponseOfStudentInfos = ApiResponseOfStudentInfos.Success(studentInfos);

                return Ok(apiResponseOfStudentInfos);
            }
            catch (Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentInfoByIdAsync(int id)
        {
            try
            {
                var studentInfo = await _service.GetStudentInfoByIdAsync(id);

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
        public async Task<IActionResult> PostStudentInfoAsync([FromBody] StudentInfo studentInfo)
        {
            if (studentInfo == null)
            {
                return BadRequest(ApiResponseOfBoolean.Error("Bad Request"));
            }

            try
            {
                var newStudentInfo = await _service.CreateStudentInfoAsync(studentInfo);

                return Ok(ApiResponseOfStudentInfo.Success(newStudentInfo));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponseOfBoolean.Error(ex.Message));
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutStudentInfoAsync([FromBody] StudentInfo studentInfo)
        {
            if (studentInfo == null)
            {
                return BadRequest(ApiResponseOfBoolean.Error("Bad Request"));
            }

            try
            {
                var isStudentInfoExist = await _service.UpdateStudentInfoAsync(studentInfo);

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
        public async Task<IActionResult> DeleteStudentInfoAsync(int id)
        {
            try
            {
                var isStudentInfoExist = await _service.DeleteStudentInfoAsync(id);

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
