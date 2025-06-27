using Microsoft.AspNetCore.Mvc;
using SAINTJWebApp.Data;
using SAINTJWebApp.Models;
using SAINTJWebApp.ViewModels;
using SAINTJWebApp.ViewModels;

namespace SAINTJWebApp.Controllers;

public class DefiController : Controller
{
    private readonly ApplicationDbContext _context;

    public DefiController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ListeDefis()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null) return RedirectToAction("Connexion", "User");

        var defis = _context.Defis
            .Select(defi => new DefiViewModel
            {
                Id = defi.id,
                Titre = defi.titre,
                Points = defi.points,
                Difficulte = defi.difficulte,
                EstAccompli = _context.UserDefis.Any(ud => ud.defiId == defi.id && ud.userId == userId)
            })
            .ToList();

        var grouped = defis
            .GroupBy(d => d.Difficulte.ToLower())
            .ToDictionary(g => g.Key, g => g.ToList());

        var user = _context.Users.FirstOrDefault(u => u.idUSer == userId);
        
        var vm = new ListeDefisViewModel
        {
            DefisParDifficulte = grouped,
            DefiAjoutRestant = user?.nbrDefiAAjouter ?? 0
        };

        return View(vm);
    }
    
    [HttpPost]
    public IActionResult MettreAJourDefis(List<int> defisAccomplis)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
            return RedirectToAction("Connexion", "User");

        var anciens = _context.UserDefis.Where(ud => ud.userId == userId);
        _context.UserDefis.RemoveRange(anciens);

        foreach (var defiId in defisAccomplis)
        {
            _context.UserDefis.Add(new UserDefi
            {
                userId = userId.Value,
                defiId = defiId
            });
        }

        _context.SaveChanges();
        return RedirectToAction("ListeDefis");
    }

    [HttpPost]
    public IActionResult ToggleDefi(int defiId)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Connexion", "User");
        }

        var user = _context.Users.FirstOrDefault(u => u.idUSer == userId);
        var defi = _context.Defis.FirstOrDefault(d => d.id == defiId);

        if (user == null || defi == null)
        {
            return NotFound(); 
        }

        //verification si le user a deja coché le defis
        var userDefi = _context.UserDefis.FirstOrDefault(ud => ud.userId == user.idUSer && ud.defiId == defiId);

        if (userDefi != null)
        {
            _context.UserDefis.Remove(userDefi);
            user.score -= defi.points; 
        }
        else
        {
            _context.UserDefis.Add(new UserDefi
            {
                userId = user.idUSer,
                defiId = defi.id
            });
            user.score += defi.points; 
        }

        _context.SaveChanges();

        return RedirectToAction("ListeDefis");
    }
    
    
    // a voir si c'est post ou get 
    public IActionResult Ajoutdefis(string titre, string description, int points, string difficulte)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
            return RedirectToAction("Connexion", "User");

        var user = _context.Users.FirstOrDefault(u => u.idUSer == userId);

        if (user == null || user.nbrDefiAAjouter <= 0)
        {
            TempData["MissingCredit"] = "Vous ne pouvez plus ajouter de défis !";
            return RedirectToAction("ListeDefis"); 
        }

        int pointsAjout = difficulte.ToLower() switch
        {
            "facile" => 10,
            "moyen" => 15,
            "difficile" => 20,
            "extreme" => 30 
        };

        var newDefi = new Defi
        {
            titre = titre,
            points = pointsAjout,
            difficulte = difficulte
        };
        _context.Defis.Add(newDefi);
        user.nbrDefiAAjouter--; 
        _context.SaveChanges();
        
        return RedirectToAction("ListeDefis");
    }
}