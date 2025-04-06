using Moq;
using UserManagement.Controllers;
using UserManagement.Models;
using UserManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace UserManagement.Test
{
    public class CourseControllerTests
    {
        private readonly Mock<ICourseRepository> _mockCourseRepo;
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly CourseController _controller;

        public CourseControllerTests()
        {
            _mockCourseRepo = new Mock<ICourseRepository>();
            _mockUserRepo = new Mock<IUserRepository>();
            _controller = new CourseController(_mockCourseRepo.Object, _mockUserRepo.Object);
        }

        [Fact]
        public void CreateCourse_Should_Succeed_When_Valid_Course()
        {
            var course = new Course { Name = "Math", Description = "Math Course", Credit = 3 };
            _mockCourseRepo.Setup(x => x.CreateCourse(course)).Returns(true);

            var result = _controller.Create(course);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ManageCourse", redirectToActionResult.ActionName);
        }

        [Fact]
        public void CreateCourse_InvalidCourse_ReturnsViewResult()
        {
            var course = new Course { Name = "", Description = "Invalid", Credit = -1 };
            _controller.ModelState.AddModelError("Name", "Required");
            var result = _controller.Create(course) as ViewResult;
            Assert.NotNull(result);
            Assert.Equal(course, result.Model);
        }


        [Fact]
        public void EditCourse_Should_Succeed_When_Valid_Course()
        {
            var course = new Course { Id = 1, Name = "Math", Description = "Math Course", Credit = 3 };
            _mockCourseRepo.Setup(x => x.UpdateCourse(course)).Returns(true);

            var result = _controller.Edit(1, course);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ManageCourse", redirectToActionResult.ActionName);
        }


        [Fact]
        public void DeleteCourse_Should_Succeed_When_Valid_Course()
        {
            _mockCourseRepo.Setup(x => x.DeleteCourse(1)).Returns(true);

            var result = _controller.DeleteConfirmed(1);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ManageCourse", redirectToActionResult.ActionName);
        }

        [Fact]
        public void ManageCourse_ReturnsViewResult_WithCourses()
        {
            var courses = new List<Course>
            {
                new Course { Id = 1, Name = "Course 1", Description = "Description 1", Credit = 3 },
                new Course { Id = 2, Name = "Course 2", Description = "Description 2", Credit = 4 }
            };
            _mockCourseRepo.Setup(repo => repo.GetAllCourses()).Returns(courses);
            var result = _controller.ManageCourse() as ViewResult;
            var model = result?.Model as List<Course>;
            Assert.NotNull(result);
            Assert.Equal(2, model?.Count);
        }
    }
}


