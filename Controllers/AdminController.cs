using Microsoft.AspNetCore.Mvc;
using SAINTJWebApp.Data;
using SAINTJWebApp.ViewModels;

namespace SAINTJWebApp.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        var defis = _context.Defis.ToList();

        var viewmodel = new AdminViewModel
        {
            Defis = defis, 
            Users = users 
        };
            
        return View(viewmodel);
    }
}