using Microsoft.EntityFrameworkCore;
using Recipes.Domain.Users.Models;

namespace Recipes.Infrastructure.Common.Data;

public class AppDbContext(DbContextOptions<DbContext> options) : DbContext(options)
{
    public DbSet<RoleModel> Roles => Set<RoleModel>();

    public DbSet<UserModel> Users => Set<UserModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<RoleModel>().ToTable("Roles", "roles");
        modelBuilder.Entity<UserModel>().ToTable("Users", "roles");
    }
}