using System;
using System.Linq;
using Domain.App;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Additive> Additive { get; set; } = default!;
        public DbSet<AdditiveInCocktail> AdditiveInCocktail { get; set; } = default!;
        public DbSet<AmountUnit> AmountUnit { get; set; } = default!;
        public DbSet<Cocktail> Cocktail { get; set; } = default!;
        public DbSet<Drink> Drink { get; set; } = default!;
        public DbSet<DrinkInCocktail> DrinkInCocktail { get; set; } = default!;
        public DbSet<DrinkType> DrinkType { get; set; } = default!;
        public DbSet<Grade> Grade { get; set; } = default!;
        public DbSet<Role> Role { get; set; } = default!;
        public DbSet<Step> Step { get; set; } = default!;
        public DbSet<User> User { get; set; } = default!;
        public DbSet<UserWithCocktail> UserWithCocktail { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}