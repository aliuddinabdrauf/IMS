using Microsoft.AspNetCore.Mvc;

namespace IMS.WebApi.Controllers;

public class AuthenticateController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}