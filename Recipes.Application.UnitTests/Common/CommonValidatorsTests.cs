using Recipes.Application.Common.Validators;

namespace Recipes.Application.UnitTests.Common;

public class CommonValidatorsTests
{
    [Theory]
    [InlineData("", false)]
    [InlineData("http://bing.com", false)]
    [InlineData("https://bing.com", true)]
    public void HttpsUrlRegex_ShouldValidate(string regex, bool isValid)
    {
        Assert.True(CommonValidators.HttpsUrlRegex.IsMatch(regex) == isValid);
    }
}