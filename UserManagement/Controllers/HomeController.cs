using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;

namespace UserManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index1()
    {
        ViewBag.IsLogin = 0;
        if (HttpContext.Session.GetInt32("IsLogin") == 1 && HttpContext.Session.GetString("Fullname") != null)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.IsLogin = 1;
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}