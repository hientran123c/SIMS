using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // GET: Course
        public IActionResult Index()
        {
            var courses = _courseRepository.GetAllCourses();
            return View(courses);
        }


        public IActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = _courseRepository.CreateCourse(course);
                if (isCreated)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error creating course");
            }
            return View(course);
        }

        // GET: Course/Edit/5
        public IActionResult Edit(int id)
        {
            var course = _courseRepository.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Course/Edit/5
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
                    return RedirectToAction(nameof(Index));
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
                return NotFound();  // Return NotFound if the course doesn't exist
            }
            return View(course); // Show confirmation page
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Call repository method to delete the course
            bool isDeleted = _courseRepository.DeleteCourse(id);
            if (isDeleted)
            {
                // Redirect to Index page after successful deletion
                return RedirectToAction(nameof(Index));
            }
            // If deletion fails, stay on the same page or show an error
            return View();
        }
    }
}

