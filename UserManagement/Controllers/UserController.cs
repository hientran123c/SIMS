using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            string username = user.Username;
            string password = user.Password;
            var _user = _userRepository.GetUserByUsernameAndPassword(username, password);

            if (_user != null)
            {
                ViewBag.Username = _user.Username;
                ViewBag.IsLogin = true;
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("Fullname", _user.Fullname);
                HttpContext.Session.SetInt32("IsLogin", 1);
            }

            return View();
        }


        //[HttpGet]
        //public IActionResult Register()
        //{
        //    var roles = _userRepository.GetRoles(); // Assuming a method to get all roles
        //    ViewBag.Roles = new SelectList(roles, "Id", "Name"); // For dropdown
        //    return View();
        //}

        //public IActionResult Register(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingUser = _userRepository.GetUserByUsername(user.Username);
        //        if (existingUser != null)
        //        {
        //            ModelState.AddModelError("", "Username is already taken.");
        //            return View();
        //        }

        //        user.CreatedAt = DateTime.Now;
        //        user.LastLoginAt = DateTime.Now;

        //        bool isCreated = _userRepository.CreateUser(user);

        //        if (isCreated)
        //        {
        //            TempData["Message"] = "Registration successful!";
        //            return RedirectToAction("ViewAccount", "Account");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Error creating the user. Please try again.");
        //        }
        //    }
        //    return View();
        //}
    }
}




