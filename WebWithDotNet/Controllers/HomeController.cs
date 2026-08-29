using Microsoft.AspNetCore.Mvc;

namespace WebWithDotNet.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}