using System.Text.RegularExpressions;

namespace Recipes.Application.Common.Validators;

internal static partial class CommonValidators
{
    internal static Regex HttpsUrlRegex = HttpsUrlRegexGenerator();

    [GeneratedRegex("^https://")]
    private static partial Regex HttpsUrlRegexGenerator();
}