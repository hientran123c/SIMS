using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Trang hiển thị thông tin tài khoản
        [HttpGet]
        public IActionResult ViewAccount()
        {

            // Lấy thông tin tài khoản người dùng từ session
            string username = HttpContext.Session.GetString("Username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "User"); // Nếu chưa đăng nhập, chuyển hướng về trang login
            }

            var users = _userRepository.GetAllUsers();
            return View(users);
        }

        // Trang xóa tài khoản
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                TempData["Message"] = "Không tìm thấy tài khoản.";
                return RedirectToAction("ViewAccount", "Account");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            bool isDeleted = _userRepository.DeleteUser(id);
            if (isDeleted)
            {
                TempData["Message"] = "Tài khoản đã bị xóa thành công!";
            }
            else
            {
                TempData["Message"] = "Có lỗi khi xóa tài khoản.";
            }

            return RedirectToAction("ViewAccount", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roles = _userRepository.GetRoles(); // Assuming a method to get all roles
            ViewBag.Roles = new SelectList(roles, "Id", "Name"); // For dropdown
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _userRepository.GetUserByUsername(user.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username is already taken.");
                    return View();
                }

                user.CreatedAt = DateTime.Now;
                user.LastLoginAt = DateTime.Now;

                bool isCreated = _userRepository.CreateUser(user);

                if (isCreated)
                {
                    TempData["Message"] = "Registration successful!";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Error creating the user. Please try again.");
                }
            }
            return View();
        }
    }
}
