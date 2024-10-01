using AutoMapper;
using Core.CourseCRUD.DTOs;
using Core.CourseCRUD.Entities;
using Core.CourseCRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.CourseCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _courseService.GetCoursesAsync();

            return Ok(courses);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] CourseDTO courseDTO)
        {
            try
            {
                var course = _mapper.Map<Course>(courseDTO);

                await _courseService.ValidateAndAddCourseAsync(course);

                return Ok(course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteCourseAsync(id);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseByDescription(string description)
        {
            var course = await _courseService.GetCourseByDescription(description);

            return Ok(course);
        }
    }

}
