using Microsoft.AspNetCore.Mvc;

namespace ApiLab.Controllers;

public class GroupsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}