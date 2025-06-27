using Microsoft.AspNetCore.Mvc;
using SAINTJWebApp.Data;
using SAINTJWebApp.ViewModels;

namespace SAINTJWebApp.Controllers;

public class ClassementController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClassementController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var usersPodium = _context.Users
            .OrderByDescending(u => u.score)
            .Select(u => new ClassementViewModel
            {
                pseudo = u.userName,
                score = u.score,
            })
            .ToList();
        return View(usersPodium);
    }
}