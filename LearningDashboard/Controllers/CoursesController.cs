using LearningDashboard.Interfaces;
using LearningDashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_courseService.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var course = _courseService.Get(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Course course)
        {
            _courseService.Add(course);
            return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Course course)
        {
            if (_courseService.Get(id) == null) return NotFound();
            course.Id = id;
            _courseService.Update(course);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_courseService.Get(id) == null) return NotFound();
            _courseService.Delete(id);
            return NoContent();
        }
    }
}
