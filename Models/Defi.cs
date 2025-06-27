using System.ComponentModel.DataAnnotations;

namespace SAINTJWebApp.Models;

public class Defi
{
    [Key]
    public int id { get; set; }
    public string titre { get; set; }
    public string difficulte { get; set; }
    public int points { get; set; }
}