using Microsoft.AspNetCore.Mvc;

namespace IMS.WebApi.Controllers;

public class CourseController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}