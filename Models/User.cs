using System.ComponentModel.DataAnnotations;

namespace SAINTJWebApp.Models;

public class User
{
    [Key]
    public int idUSer { get; set; }
    public string userName { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public int score { get; set; }
    public int nbrDefiAAjouter { get; set; } 
    // public string role { get; set; }
}