using Microsoft.EntityFrameworkCore;
using SAINTJWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Ajouter la BDD PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter les services MVC + TempData pour les sessions
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();

// Ajouter la gestion des sessions
builder.Services.AddSession();

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Ajouter ici avant Routing
app.UseSession();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();