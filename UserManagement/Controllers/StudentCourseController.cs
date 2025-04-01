using Microsoft.AspNetCore.Mvc;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentCourseController : ControllerBase
    {
        private readonly StudentCourseService _service;

        public StudentCourseController(StudentCourseService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult AddCourseForStudent(int studentId, int courseId, DateTime beginTime, DateTime endTime)
        {
            _service.AddCourseForStudent(studentId, courseId, beginTime, endTime);
            return Ok("Course added successfully.");
        }

        [HttpGet("{studentId}")]
        public IActionResult GetCoursesForStudent(int studentId)
        {
            var courses = _service.GetCoursesForStudent(studentId);
            return Ok(courses);
        }
    }
}
