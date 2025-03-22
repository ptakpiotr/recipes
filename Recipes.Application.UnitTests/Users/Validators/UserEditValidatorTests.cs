using Recipes.Application.UnitTests.Users.Validators.Fixtures;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Validators;

namespace Recipes.Application.UnitTests.Users.Validators;

public class UserEditValidatorTests(UserEditValidator validator) : UserEditValidatorFixture
{
    [Theory]
    [InlineData("1BB5204C-4FB0-4079-9DB4-2FC7436853B9")]
    [InlineData("E8432368-9894-46F1-B857-64432A00AB59")]
    public void ProperUserEditData_Should_BeValid(string userId)
    {
        var dto = new UserEditDto()
        {
            Id = Guid.Parse(userId)
        };

        var res = validator.Validate(dto);

        Assert.True(res.IsValid);
    }

    [Fact]
    public void UserEditData_Should_BeInvalid()
    {
        var dto = new UserEditDto()
        {
            Id = Guid.Empty
        };

        var res = validator.Validate(dto);

        Assert.False(res.IsValid);
    }
}