using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Infrastructure.Recipes.EntityConfigurations;

public class IngredientsEntityConfiguration : IEntityTypeConfiguration<IngredientModel>
{
    public void Configure(EntityTypeBuilder<IngredientModel> builder)
    {
        builder.HasKey(x => x.Id);
    }
}