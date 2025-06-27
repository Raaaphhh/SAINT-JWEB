using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SAINTJWebApp.Data;
using SAINTJWebApp.Models;
using System.Linq;

namespace SAINTJWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Inscription()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inscription(string userName, string email, string password, string confirmPassword)
        {
            if (_context.Users.Any(u => u.email == email))
            {
                TempData["CompteDejaUse"] = "Cet email est déjà utilisé.";
                return View();
            }

            if (password != confirmPassword)
            {
                TempData["CompteDejaUse"] = "Les mots de passe ne correspondent pas.";
                return View();
            }

            var passwordHasher = new PasswordHasher<User>();

            var newuser = new User
            {
                userName = userName,
                email = email,
                nbrDefiAAjouter = 5 
            };

            newuser.password = passwordHasher.HashPassword(newuser, password);

            _context.Users.Add(newuser);
            _context.SaveChanges();

            TempData["CreateAccountSucc"] = "Compte créé avec succès";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Connexion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Connexion(string userName, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.userName == userName);
            if (user == null)
            {
                TempData["LoginError"] = "Pseudo invalide.";
                return View();
            }

            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.password, password);


            if (result == PasswordVerificationResult.Success)
            {
                TempData["LoginSuccess"] = $"Bienvenue {user.userName} !";
                HttpContext.Session.SetInt32("UserId", user.idUSer);
                return RedirectToAction("Index", "Menu");
            }

            TempData["LoginError"] = "Mot de passe incorrect.";
            return RedirectToAction("Connexion", "User");
        }
    }
}
