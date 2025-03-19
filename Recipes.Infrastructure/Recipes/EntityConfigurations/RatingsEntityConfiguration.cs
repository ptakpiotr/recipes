using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Infrastructure.Recipes.EntityConfigurations;

public class RatingsEntityConfiguration : IEntityTypeConfiguration<RatingModel>
{
    public void Configure(EntityTypeBuilder<RatingModel> builder)
    {
        builder.HasKey(x => x.Id);
    }
}