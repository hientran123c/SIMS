using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Data;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentCourseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentCourseController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddCourseForStudent(int studentId, int courseId, DateTime beginTime, DateTime endTime)
        {
            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId,
                BeginTime = beginTime,
                EndTime = endTime
            };
            _context.StudentCourses.Add(studentCourse);
            _context.SaveChanges();
            return Ok("Course added successfully.");
        }

        [HttpGet("{studentId}")]
        public IActionResult GetCoursesForStudent(int studentId)
        {
            var courses = _context.StudentCourses
                .Where(sc => sc.StudentId == studentId)
                .ToList();
            return Ok(courses);
        }

        [HttpGet]
        public IActionResult GetAllStudentCourses()
        {
            var studentCourses = _context.StudentCourses.ToList();
            return Ok(studentCourses);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudentCourse(int id, [FromBody] StudentCourse updatedCourse)
        {
            var existingCourse = _context.StudentCourses.Find(id);
            if (existingCourse == null)
                return NotFound("StudentCourse not found.");

            existingCourse.StudentId = updatedCourse.StudentId;
            existingCourse.CourseId = updatedCourse.CourseId;
            existingCourse.BeginTime = updatedCourse.BeginTime;
            existingCourse.EndTime = updatedCourse.EndTime;

            _context.SaveChanges();
            return Ok("StudentCourse updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudentCourse(int id)
        {
            var studentCourse = _context.StudentCourses.Find(id);
            if (studentCourse == null)
                return NotFound("StudentCourse not found.");

            _context.StudentCourses.Remove(studentCourse);
            _context.SaveChanges();
            return Ok("StudentCourse deleted successfully.");
        }
    }
}
