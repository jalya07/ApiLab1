using Microsoft.AspNetCore.Mvc;

namespace ApiLab.Controllers;

public class StudentsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}