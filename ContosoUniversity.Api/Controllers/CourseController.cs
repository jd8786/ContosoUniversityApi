using ContosoUniversity.Api.Models;
using ContosoUniversity.Api.Services;
using ContosoUniversity.Data.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace ContosoUniversity.Api.Controllers
{
    [Route("api/courses")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<Course>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult GetCourses()
        {
            try
            {
                var courses = _courseService.GetAll();

                var apiResponse = ApiResponse<IEnumerable<Course>>.Success(courses);

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ApiResponse<bool>.Error(ex.Message));
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Course>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult GetCourseById(int id)
        {
            try
            {
                var course = _courseService.Get(id);

                var apiResponse = ApiResponse<Course>.Success(course);

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
        [ProducesResponseType(typeof(ApiResponse<Course>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 400)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult PostCourse([FromBody] Course course)
        {
            try
            {
                var newCourse = _courseService.Add(course);

                return Ok(ApiResponse<Course>.Success(newCourse));
            }
            catch (InvalidCourseException ex)
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
        [ProducesResponseType(typeof(ApiResponse<Course>), 200)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 400)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 404)]
        [ProducesResponseType(typeof(ApiResponse<bool>), 500)]
        public IActionResult PutCourse([FromBody] Course course)
        {
            try
            {
                var updatedCourse = _courseService.Update(course);

                return Ok(ApiResponse<Course>.Success(updatedCourse));
            }
            catch (InvalidCourseException ex)
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
        public IActionResult DeleteCourse(int id)
        {
            try
            {
                _courseService.Remove(id);

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
