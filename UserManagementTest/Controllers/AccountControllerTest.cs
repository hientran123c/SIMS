using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Controllers;
using UserManagement.Repositories;
using UserManagement.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace UserManagement.Test
{
    public class AccountControllerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly AccountController _accountController;
        private readonly Mock<ITempDataDictionary> _tempDataMock;

        public AccountControllerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _tempDataMock = new Mock<ITempDataDictionary>();
            _accountController = new AccountController(_userRepositoryMock.Object)
            {
                TempData = _tempDataMock.Object
            };
        }

        [Fact]
        public void EditUser_ValidUser_ReturnsRedirectToAction()
        {
            var user = new User { Id = 1, Username = "user1", Fullname = "User One", RoleId = 1 };
            _userRepositoryMock.Setup(repo => repo.UpdateUser(It.IsAny<User>())).Returns(true);
            var result = _accountController.Edit(1, user) as RedirectToActionResult;
            Assert.NotNull(result);
            Assert.Equal("ViewAccount", result.ActionName);
        }

        [Fact]
        public void Edit_ReturnsNotFound_WhenUserDoesNotExist()
        {
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns((User)null);
            var result = _accountController.Edit(userId);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_ReturnsViewResult_WhenUserExists()
        {
            int userId = 1;
            var user = new User { Id = userId, Username = "testuser", Fullname = "Test User" };
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(user);
            var result = _accountController.Edit(userId);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(user, viewResult.Model);
        }

        [Fact]
        public void Edit_Post_ReturnsRedirectToAction_WhenUserIsUpdated()
        {
            int userId = 1;
            var user = new User { Id = userId, Username = "testuser", Fullname = "Test User" };
            _userRepositoryMock.Setup(repo => repo.UpdateUser(user)).Returns(true);
            var result = _accountController.Edit(userId, user);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ViewAccount", redirectResult.ActionName);
        }

        [Fact]
        public void Edit_Post_ReturnsView_WhenUpdateFails()
        {
            int userId = 1;
            var user = new User { Id = userId, Username = "testuser", Fullname = "Test User" };
            _userRepositoryMock.Setup(repo => repo.UpdateUser(user)).Returns(false);
            var result = _accountController.Edit(userId, user);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(user, viewResult.Model);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenUserDoesNotExist()
        {
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns((User)null);
            var result = _accountController.Delete(userId);
            Assert.IsType<RedirectToActionResult>(result);
            var redirectResult = (RedirectToActionResult)result;
            Assert.Equal("ViewAccount", redirectResult.ActionName);
        }

        [Fact]
        public void Delete_ReturnsViewResult_WhenUserExists()
        {
            int userId = 1;
            var user = new User { Id = userId, Username = "testuser", Fullname = "Test User" };
            _userRepositoryMock.Setup(repo => repo.GetUserById(userId)).Returns(user);
            var result = _accountController.Delete(userId);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(user, viewResult.Model);
        }

        [Fact]
        public void DeleteConfirmed_ReturnsRedirectToAction_WhenUserIsDeleted()
        {
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.DeleteUser(userId)).Returns(true);
            var result = _accountController.DeleteConfirmed(userId);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ViewAccount", redirectResult.ActionName);
        }

        [Fact]
        public void Register_ValidUser_ReturnsSuccessMessage()
        {
            var user = new User
            {
                Username = "newuser",
                Password = "Password123",
                ConfirmPassword = "Password123",
                Fullname = "New User",
                RoleId = 1,
                CreatedAt = DateTime.Now,
                LastLoginAt = DateTime.Now
            };

            _userRepositoryMock.Setup(repo => repo.CreateUser(user)).Returns(true);
            var result = _accountController.Register(user) as ViewResult;
            Assert.NotNull(result);
            Assert.Contains("Registration successful!", _tempDataMock.Object.Values);
        }

        [Fact]
        public void Register_PasswordsDoNotMatch_ReturnsErrorMessage()
        {
            // Arrange
            var user = new User
            {
                Username = "newuser",
                Password = "Password123",
                ConfirmPassword = "Password124", // Mismatched confirm password
                Fullname = "New User",
                RoleId = 1
            };

            // Act
            var result = _accountController.Register(user) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains("The password and confirmation password do not match.", result.ViewData.ModelState["ConfirmPassword"].Errors[0].ErrorMessage);
        }

        [Fact]
        public void Register_ExistingUsername_ReturnsErrorMessage()
        {
            // Arrange
            var user = new User
            {
                Username = "existinguser",
                Password = "Password123",
                ConfirmPassword = "Password123",
                Fullname = "Existing User",
                RoleId = 1
            };

            _userRepositoryMock.Setup(repo => repo.GetUserByUsername(user.Username)).Returns(user);

            // Act
            var result = _accountController.Register(user) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains("Username is already taken.", result.ViewData.ModelState[""].Errors[0].ErrorMessage);
        }

    }
}
