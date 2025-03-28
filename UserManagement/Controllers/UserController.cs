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
                switch (_user.RoleId)
                {
                    case 1: 
                        return RedirectToAction("AdminPage", "Home");
                    case 2:
                        return RedirectToAction("StudentPage", "Home");
                    case 3: 
                        return RedirectToAction("FacultyPage", "Home");
                    default:
                        return RedirectToAction("Login", "User");
                }
            }
            return View();
        }
    }
}




