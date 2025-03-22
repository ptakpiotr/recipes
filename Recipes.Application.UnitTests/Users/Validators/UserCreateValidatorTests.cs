using Recipes.Application.UnitTests.Users.Validators.Fixtures;
using Recipes.Application.Users.DTO;
using Recipes.Application.Users.Validators;

namespace Recipes.Application.UnitTests.Users.Validators;

public class UserCreateValidatorTests(UserCreateValidator validator) : UserCreateValidatorFixture
{
    [Theory]
    [MemberData(nameof(UserValidDataCreator))]
    public void ProperUserData_Should_BeValid(string externalId, string userName, string userImageLink)
    {
        var dto = new UserCreateDto()
        {
            ExternalId = externalId,
            UserName = userName,
            UserImageLink = userImageLink,
            SendNewsletter = false
        };

        var res = validator.Validate(dto);

        Assert.True(res.IsValid);
    }

    [Theory]
    [MemberData(nameof(UserInvalidDataCreator))]
    public void ProperUserData_Should_BeInvalid(string externalId, string userName, string userImageLink)
    {
        var dto = new UserCreateDto()
        {
            ExternalId = externalId,
            UserName = userName,
            UserImageLink = userImageLink,
            SendNewsletter = false
        };

        var res = validator.Validate(dto);

        Assert.False(res.IsValid);
    }

    public static IEnumerable<object[]> UserValidDataCreator()
    {
        yield return [Guid.NewGuid().ToString(), "test", "https://bing.com"];
        yield return [Guid.NewGuid().ToString(), "test234", "https://bing.com"];
    }

    public static IEnumerable<object[]> UserInvalidDataCreator()
    {
        yield return [Guid.NewGuid().ToString(), "test", "http://bing.com"];
        yield return [string.Empty, "test", "http://bing.com"];
        yield return [Guid.NewGuid().ToString(), string.Empty, "https://bing.com"];
        yield return [string.Empty, string.Empty, string.Empty];
    }
}