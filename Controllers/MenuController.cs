using Microsoft.AspNetCore.Mvc;
using SAINTJWebApp.Data;
using SAINTJWebApp.Models;
using SAINTJWebApp.ViewModels;
using SAINTJWebApp.ViewModels;

namespace SAINTJWebApp.Controllers;
public class MenuController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public MenuController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId != null)
        {
            var score = _context.Users
                .Where(u => u.idUSer == userId)
                .Select(u => u.score)
                .FirstOrDefault();

            ViewBag.Score = score;
        }

        return View();
    }

}
