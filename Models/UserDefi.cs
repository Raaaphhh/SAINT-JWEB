using System.ComponentModel.DataAnnotations;

namespace SAINTJWebApp.Models;

public class UserDefi
{
    [Key]
    public int id { get; set; }
    
    public int userId { get; set; }
    public User User { get; set; }
    
    public int defiId { get; set; }
    public Defi Defi { get; set; }
    
    public bool estAccompli { get; set; }
}