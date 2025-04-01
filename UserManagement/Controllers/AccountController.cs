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

        [HttpGet]
        public IActionResult ViewAccount()
        {
            string? username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "User");
            }

            var users = _userRepository.GetAllUsers();
            var roles = _userRepository.GetRoles();
            var roleDictionary = roles.ToDictionary(r => r.Id, r => r.Name);
            ViewBag.RoleDictionary = roleDictionary;
            return View(users);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = _userRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Name", user.RoleId); // Include roles in the view

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool isUpdated = _userRepository.UpdateUser(user);
                if (isUpdated)
                {
                    return RedirectToAction("ViewAccount", "Account");
                }

                ModelState.AddModelError("", "Error updating user.");
            }
            var roles = _userRepository.GetRoles();
            ViewBag.Roles = new SelectList(roles, "Id", "Name", user.RoleId); 
            return View(user);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("ViewAccount", "Account");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
             _userRepository.DeleteUser(id);
            return RedirectToAction("ViewAccount", "Account");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roles = _userRepository.GetRoles(); 
            ViewBag.Roles = new SelectList(roles, "Id", "Name"); 
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Password != user.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
                    return View();
                }

                var existingUser = _userRepository.GetUserByUsername(user.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username is already taken.");
                    var roles = _userRepository.GetRoles();
                    ViewBag.Roles = new SelectList(roles, "Id", "Name", user.RoleId); 
                    return View(user);
                }

                user.CreatedAt = DateTime.Now;
                user.LastLoginAt = DateTime.Now;
                bool isCreated = _userRepository.CreateUser(user);
                if (isCreated)
                {
                    TempData["RegisterMessage"] = "Registration successful!";
                    TempData["Redirect"] = Url.Action("ViewAccount", "Account");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Error creating the user. Please try again.");
                    var roles = _userRepository.GetRoles();
                    ViewBag.Roles = new SelectList(roles, "Id", "Name", user.RoleId);
                }
            }
            else
            {
                var roles = _userRepository.GetRoles();
                ViewBag.Roles = new SelectList(roles, "Id", "Name", user.RoleId);
            }
            return View(user);
        }
    }
}
