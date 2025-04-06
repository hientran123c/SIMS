using Moq;
using Xunit;
using UserManagement.Controllers;
using UserManagement.Models;
using UserManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace UserManagement.Test
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly UserController _controller;
        private readonly Mock<HttpContext> _mockHttpContext;
        private readonly Mock<ISession> _mockSession;

        public UserControllerTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _mockHttpContext = new Mock<HttpContext>();
            _mockSession = new Mock<ISession>();

            _controller = new UserController(_mockUserRepo.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = _mockHttpContext.Object
                }
            };

            _mockHttpContext.Setup(c => c.Session).Returns(_mockSession.Object);
        }


        [Fact]
        public void Login_InvalidUser_ReturnsViewWithErrorMessage()
        {
            var username = "testuser";
            var password = "wrongpassword";
            _mockUserRepo.Setup(repo => repo.GetUserByUsernameAndPassword(username, password)).Returns((User)null);

            var model = new User { Username = username, Password = password };
            var result = _controller.Login(model) as ViewResult;
            Assert.NotNull(result);
            Assert.Null(result.ViewData["Fullname"]);
            Assert.False(result.ViewData.ContainsKey("IsLogin"));
        }

        [Fact]
        public void Login_ValidUser_ReturnsViewWithUserDetails()
        {
            // Arrange
            var username = "testuser";
            var password = "correctpassword";
            var user = new User { Username = username, Password = password, Fullname = "Test User" };
            _mockUserRepo.Setup(repo => repo.GetUserByUsernameAndPassword(username, password)).Returns(user);

            var model = new User { Username = username, Password = password };

            // Act
            var result = _controller.Login(model) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test User", result.ViewData["Fullname"]);
            Assert.True((bool)result.ViewData["IsLogin"]);
        }
    }
}


