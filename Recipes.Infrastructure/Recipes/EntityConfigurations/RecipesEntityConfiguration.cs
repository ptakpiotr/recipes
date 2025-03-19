using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Infrastructure.Recipes.EntityConfigurations;

public class RecipesEntityConfiguration : IEntityTypeConfiguration<RecipeModel>
{
    public void Configure(EntityTypeBuilder<RecipeModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");
        builder.Property(x => x.UpdatedAt).HasDefaultValueSql("NOW()");

        builder.HasMany(i => i.Ingredients)
            .WithOne(i => i.Recipe)
            .HasForeignKey(i => i.RecipeId);

        builder.HasMany(r => r.Ratings)
            .WithOne(r => r.Recipe)
            .HasForeignKey(r => r.RecipeId);
    }
}