using LearningDashboard.Interfaces;
using LearningDashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrolmentsController : ControllerBase
    {
        private readonly IEnrolmentService _enrolmentService;

        public EnrolmentsController(IEnrolmentService enrolmentService)
        {
            _enrolmentService = enrolmentService;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_enrolmentService.GetAll());

        [HttpGet("student/{studentId}")]
        public IActionResult GetByStudent(int studentId) =>
            Ok(_enrolmentService.GetByStudent(studentId));

        [HttpGet("course/{courseId}")]
        public IActionResult GetByCourse(int courseId) =>
            Ok(_enrolmentService.GetByCourse(courseId));

        [HttpPost]
        public IActionResult Enrol([FromBody] Enrolment enrolment)
        {
            _enrolmentService.EnrolStudent(enrolment.StudentId, enrolment.CourseId);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Unenrol([FromBody] Enrolment enrolment)
        {
            _enrolmentService.RemoveEnrolment(enrolment.StudentId, enrolment.CourseId);
            return NoContent();
        }

        [HttpGet("report")]
        public ActionResult<List<EnrolmentReportItem>> GetReport()
        {
            var report = _enrolmentService.GetEnrolmentReport();
            return Ok(report);
        }

    }
}
