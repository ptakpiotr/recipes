using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Infrastructure.Recipes.DbEntities;

namespace Recipes.Infrastructure.Recipes.EntityConfigurations;

public class RecipeVectorsConfiguration : IEntityTypeConfiguration<RecipeVectors>
{
    public void Configure(EntityTypeBuilder<RecipeVectors> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Vector)
            .HasColumnType("vector(1024)")
            .IsRequired();
    }
}