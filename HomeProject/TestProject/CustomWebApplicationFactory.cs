using System.Linq;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // find the database context
                var descriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                if (descriptor != null) services.Remove(descriptor);
                
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase(builder.GetSetting("test_database_name"));
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedService = scope.ServiceProvider;
                var db = scopedService.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();

                if (db.Additive.Any()) return;
                
                db.Cocktail.Add(new Cocktail
                {
                    Name = "Long Drink",
                    Alcoholic = true
                });
                db.Cocktail.Add(new Cocktail
                {
                    Name = "Margarita",
                    Alcoholic = true
                });
                db.Cocktail.Add(new Cocktail
                {
                    Name = "Black Russian",
                    Alcoholic = true
                });
                
                db.SaveChanges();
            });
        }
    }
}
