using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json.Converters;
using Recipes.Domain.Users.Enums;
using Recipes.Domain.Users.Models;

namespace Recipes.Infrastructure.Users.EntityConfigurations;

public class RolesEntityConfiguration : IEntityTypeConfiguration<RoleModel>
{
    public void Configure(EntityTypeBuilder<RoleModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

        builder.Property(x => x.Role)
            .HasConversion((roleType) => roleType.ToString(),
                roleType => roleType == "Admin" ? RoleType.Admin : RoleType.User);

        builder.HasIndex(x => x.Role).IsUnique();
    }
}