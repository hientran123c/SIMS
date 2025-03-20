using Microsoft.AspNetCore.Mvc;
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

        public ActionResult Index()
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
        
        [HttpGet]
        public IActionResult Register()
        {
            return View(); 
        }
        // POST: Register
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if username already exists
                var existingUser = _userRepository.GetUserByUsername(user.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username is already taken.");
                    return View();
                }

                // Set default values for RoleId and CreatedAt (LastLoginAt can also be set to DateTime.Now if necessary)
                user.RoleId = 2; // For example, set to a default RoleId (like "User")
                user.CreatedAt = DateTime.Now;
                user.LastLoginAt = DateTime.Now;

                // Save the user
                bool isCreated = _userRepository.CreateUser(user);

                if (isCreated)
                {
                    TempData["Message"] = "Registration successful!";
                    return RedirectToAction("Login"); // Redirect to the Login page after successful registration
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

