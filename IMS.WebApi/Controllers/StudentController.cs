using Microsoft.AspNetCore.Mvc;

namespace IMS.WebApi.Controllers;

public class StudentController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}