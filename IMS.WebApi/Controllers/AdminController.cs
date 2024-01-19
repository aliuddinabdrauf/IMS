using Microsoft.AspNetCore.Mvc;

namespace IMS.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}