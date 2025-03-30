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
            ViewData["Layout"] = "_Layout1";
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
                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("Fullname", _user.Fullname);
                HttpContext.Session.SetInt32("IsLogin", 1);

                ViewBag.Username = _user.Username;
                ViewBag.IsLogin = true;
                ViewBag.RoleId = _user.RoleId;  
                return View(); 
            }
            return View();
        }
    }
}




