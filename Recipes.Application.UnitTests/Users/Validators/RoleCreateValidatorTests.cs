using Recipes.Application.UnitTests.Users.Validators.Fixtures;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Validators;
using Recipes.Domain.Users.Enums;

namespace Recipes.Application.UnitTests.Users.Validators;

public class RoleCreateValidatorTests(RoleCreateValidator validator) : RoleCreateValidatorFixture
{
    [Theory]
    [InlineData("abc")]
    [InlineData("def")]
    public void NonEmptyRoleName_ValidRoleName_ShouldReturnTrue(string roleName)
    {
        var dto = new RoleCreateDto()
        {
            Name = roleName,
            Role = RoleType.User
        };

        var res = validator.Validate(dto);

        Assert.True(res.IsValid);
    }

    [Fact]
    public void EmptyRoleName_ValidRoleName_ShouldBeFalse()
    {
        var dto = new RoleCreateDto()
        {
            Name = string.Empty,
            Role = RoleType.User
        };

        var res = validator.Validate(dto);

        Assert.False(res.IsValid);
    }
}