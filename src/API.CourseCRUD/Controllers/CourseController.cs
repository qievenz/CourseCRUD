using API.CourseCRUD.Profiles;
using AutoMapper;
using Core.CourseCRUD.Entities;
using Core.CourseCRUD.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.CourseCRUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

                await _courseService.AddCourseAsync(course);

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

        // Otras acciones: buscar curso, etc.
    }

}
