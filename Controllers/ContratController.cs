using Microsoft.AspNetCore.Mvc;

namespace SAINTJWebApp.Controllers;

public class ContratController : Controller
{
    // GET
    public IActionResult ContratVide()
    {
        return View();
    }

    public IActionResult ContratGenerate()
    {
        return View();
    }
}