using Microsoft.EntityFrameworkCore;
using SAINTJWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔁 Convertir DATABASE_URL (Railway) vers une connection string PostgreSQL
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');

    var connectionString =
        $"Host={uri.Host};Port={uri.Port};Username={userInfo[0]};Password={userInfo[1]};Database={uri.AbsolutePath.TrimStart('/')};SSL Mode=Require;Trust Server Certificate=true;";

    builder.Configuration["ConnectionStrings:Default"] = connectionString;
}

// ✅ Ajouter la BDD PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// ✅ Ajouter les services MVC + TempData pour les sessions
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();

// ✅ Ajouter la gestion des sessions
builder.Services.AddSession();

var app = builder.Build();

// ✅ Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();