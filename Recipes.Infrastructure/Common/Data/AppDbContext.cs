using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Recipes.Models;
using Recipes.Domain.Users.Models;

namespace Recipes.Infrastructure.Common.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<RoleModel> Roles => Set<RoleModel>();

    public DbSet<UserModel> Users => Set<UserModel>();

    public DbSet<IngredientModel> Ingredients => Set<IngredientModel>();

    public DbSet<RatingModel> Ratings => Set<RatingModel>();

    public DbSet<RecipeModel> Recipes => Set<RecipeModel>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<RoleModel>().ToTable("Roles", "roles");
        modelBuilder.Entity<UserModel>().ToTable("Users", "roles");
        modelBuilder.Entity<IngredientModel>().ToTable("Ingredients", "recipes");
        modelBuilder.Entity<RatingModel>().ToTable("Ratings", "recipes");
        modelBuilder.Entity<RecipeModel>().ToTable("Recipes", "recipes");
    }
}