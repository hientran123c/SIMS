using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using UserManagement.Migrations;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public CourseController(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public IActionResult ManageCourse()
        {
            var courses = _courseRepository.GetAllCourses();
            return View(courses);
        }

        public IActionResult CourseView()
        {
            var courses = _courseRepository.GetAllCourses();
            return View(courses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = _courseRepository.CreateCourse(course);
                if (isCreated)
                {
                    return RedirectToAction(nameof(ManageCourse));
                }
                ModelState.AddModelError("", "Error creating course");
            }
            return View(course);
        }

        public IActionResult Edit(int id)
        {
            var course = _courseRepository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool isUpdated = _courseRepository.UpdateCourse(course);
                if (isUpdated)
                {
                    return RedirectToAction(nameof(ManageCourse));
                }
                ModelState.AddModelError("", "Error updating course");
            }
            return View(course);
        }

        public IActionResult Delete(int id)
        {
            var course = _courseRepository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = _courseRepository.DeleteCourse(id);
            if (isDeleted)
            {
                return RedirectToAction(nameof(ManageCourse));
            }
            return View();
        }

        public IActionResult AssignCourse()
        {
            var users = _userRepository.GetAllUsers().Where(u => u.RoleId == 2).ToList();
            var courses = _courseRepository.GetAllCourses().ToList();
            if (users == null || courses == null)
            {
                ModelState.AddModelError("", "No users or courses found.");
                return View();
            }
            ViewBag.Users = users; 
            ViewBag.Courses = courses;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignCourse(int userId, int courseId)
        {
            bool isAssigned = _userRepository.AssignCourseToUser(userId, courseId);
            if (isAssigned)
            {
                return RedirectToAction("ManageCourse"); 
            }
            else
            {
                ModelState.AddModelError("", "Assignment failed. Ensure the user has the correct role.");
                return View();
            }
        }

        public IActionResult UserCourseList()
        {
            var userCourses = _userRepository.GetUserCourses();
            if (userCourses == null || !userCourses.Any())
            {
                ModelState.AddModelError("", "No data found.");
                return View();
            }
            return View(userCourses);
        }
    }
}

