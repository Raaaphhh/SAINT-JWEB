using Microsoft.AspNetCore.Mvc;

namespace SAINTJWebApp.Controllers;

public class BurgerMenuController : Controller
{
    // GET
    public IActionResult Credit()
    {
        return View();
    }

    public IActionResult Apropos()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
}