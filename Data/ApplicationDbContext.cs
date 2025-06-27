using Microsoft.EntityFrameworkCore;
using SAINTJWebApp.Models; 

namespace SAINTJWebApp.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } 
    public DbSet<Defi> Defis { get; set; }
    public DbSet<UserDefi> UserDefis { get; set; }
}
