using Microsoft.EntityFrameworkCore;
using WebApp.Middleware;
using WebApp.Models.Movies;

namespace WebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<MoviesContext>(options =>
        {
            options.UseSqlite(builder.Configuration["MoviesContext:ConnectionString"]);
        });

        builder.Services.AddRazorPages();
        builder.Services.AddControllersWithViews();

        builder.Services.AddMemoryCache();
        builder.Services.AddSession();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseMiddleware<LastVisitCookie>();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseSession();

        app.MapRazorPages();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Movie}/{action=Index}/{id?}");

        app.Run();
    }
}