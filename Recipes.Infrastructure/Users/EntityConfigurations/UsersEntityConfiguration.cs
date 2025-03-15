using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes.Domain.Users.Models;

namespace Recipes.Infrastructure.Users.EntityConfigurations;

public class UsersEntityConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x=>x.ExternalId).IsRequired();
        
        builder.Property(x=>x.UserName).HasMaxLength(100).IsRequired();

        builder.Property(x=>x.UserImageLink).HasMaxLength(255).IsRequired();

        builder.HasOne<RoleModel>("Role")
            .WithMany("Users");
    }
}