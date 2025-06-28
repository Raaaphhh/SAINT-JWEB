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
        var defisParDifficulte = defis
            .GroupBy(d => d.difficulte)
            .ToDictionary(g => g.Key, g => g.ToList());


        var viewmodel = new AdminViewModel
        {
            Defis = defis, 
            Users = users,
            DefisParDifficulte = defisParDifficulte
        };
            
        return View(viewmodel);
    }

    public IActionResult ModifierScore(int idUSer, int score)
    {
        var user = _context.Users.FirstOrDefault(u => u.idUSer == idUSer);

        if (user != null)
        {
            user.score = score;
            _context.SaveChanges();
        }
        
        return RedirectToAction("Index");
    }
    
    public IActionResult ModifierDefis(int idDefis, string difficulte, string titre)
    {
        var defis = _context.Defis.FirstOrDefault(u => u.id == idDefis);

        int points = difficulte.ToLower() switch
        {
            "facile" => 10,
            "moyen" => 15,
            "difficile" => 20,
            "extreme" => 30
        }; 

        if (defis != null)
        {
            defis.titre = titre;
            defis.difficulte = difficulte;
            defis.points = points;
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}