using Microsoft.EntityFrameworkCore;
using RetroSum.Components;
using RetroSum.Data.Context;
using RetroSum.Data.Services;

namespace RetroSum {
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var relativeDbPath = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data/retro.db";
            
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeDbPath.Replace("Data Source=", ""));
            Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
            
            builder.Services.AddDbContext<RetroSumContext>(options => options.UseSqlite($"Data Source={dbPath}"));
            
            builder.Services.AddScoped<RetroService>();

            var app = builder.Build();
            
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<RetroSumContext>();
                db.Database.Migrate();
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}