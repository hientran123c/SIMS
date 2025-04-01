// using Microsoft.AspNetCore.Mvc;
// using UserManagement.Models;
// using UserManagement.Data;

// namespace UserManagement.Controllers
// {
//     [ApiController]
//     [Route("[controller]")]
//     public class StudentCourseController : Controller
//     {
//         private readonly AppDbContext _context;

//         public StudentCourseController(AppDbContext context)
//         {
//             _context = context;
//         }

//         // Factory Method for creating StudentCourse
//         protected virtual StudentCourse CreateStudentCourse(int studentId, int courseId, DateTime beginTime, DateTime endTime)
//         {
//             return new StudentCourse
//             {
//                 StudentId = studentId,
//                 CourseId = courseId,
//                 BeginTime = beginTime,
//                 EndTime = endTime
//             };
//         }

//         [HttpPost("AddCourseForStudent")]
//         public IActionResult AddCourseForStudent(int studentId, int courseId, DateTime beginTime, DateTime endTime)
//         {
//             var studentCourse = CreateStudentCourse(studentId, courseId, beginTime, endTime);
//             _context.StudentCourses.Add(studentCourse);
//             _context.SaveChanges();
//             return RedirectToAction("Index");
//         }

//         [HttpGet("Index")]
//         public IActionResult Index()
//         {
//             var studentCourses = _context.StudentCourses.ToList();
//             return View(studentCourses);
//         }

//         [HttpGet("Create")]
//         public IActionResult Create()
//         {
//             return View();
//         }

//         [HttpPost("Create")]
//         public IActionResult Create(StudentCourse studentCourse)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.StudentCourses.Add(studentCourse);
//                 _context.SaveChanges();
//                 return RedirectToAction("Index");
//             }
//             return View(studentCourse);
//         }

//         [HttpGet("Edit/{id}")]
//         public IActionResult Edit(int id)
//         {
//             var studentCourse = _context.StudentCourses.Find(id);
//             if (studentCourse == null)
//                 return NotFound();

//             return View(studentCourse);
//         }

//         [HttpPost("Edit")]
//         public IActionResult Edit(StudentCourse studentCourse)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.StudentCourses.Update(studentCourse);
//                 _context.SaveChanges();
//                 return RedirectToAction("Index");
//             }
//             return View(studentCourse);
//         }

//         [HttpGet("Delete/{id}")]
//         public IActionResult Delete(int id)
//         {
//             var studentCourse = _context.StudentCourses.Find(id);
//             if (studentCourse == null)
//                 return NotFound();

//             return View(studentCourse);
//         }

//         [HttpPost("Delete")]
//         public IActionResult DeleteConfirmed(int id)
//         {
//             var studentCourse = _context.StudentCourses.Find(id);
//             if (studentCourse != null)
//             {
//                 _context.StudentCourses.Remove(studentCourse);
//                 _context.SaveChanges();
//             }
//             return RedirectToAction("Index");
//         }

//         [HttpGet("StudentCourseView")]
//         public IActionResult StudentCourseView()
//         {
//             var studentCourses = _context.StudentCourses.ToList();
//             return View(studentCourses);
//         }
//     }
// }
